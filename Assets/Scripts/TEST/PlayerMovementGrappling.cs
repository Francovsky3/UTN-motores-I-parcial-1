using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerMovementGrappling : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 7f;
    public float groundDrag = 5f;

    [Header("Jumping")]
    public float jumpForce = 12f;
    public float jumpCooldown = 0.25f;
    public float airMultiplier = 0.4f;
    private bool readyToJump = true;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask whatIsGround;
    private bool grounded;

    [Header("References")]
    public Transform orientation;
    public ThirdPersonCam cam;

    [Header("Grappling State")]
    public bool freeze;
    public bool activeGrapple;
    private bool enableMovementOnNextTouch;
    private Vector3 velocityToSet;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;
    private Rigidbody rb;

    public MovementState state;
    public enum MovementState
    {
        walking,
        air,
        grappling,
        freeze
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        // Ground Check usando CheckSphere
        Vector3 checkPos = groundCheck != null ? groundCheck.position : transform.position - new Vector3(0, 1f, 0);
        grounded = Physics.CheckSphere(checkPos, groundDistance, whatIsGround);

        MyInput();
        SpeedControl();
        StateHandler();

        // Manejo de rozamiento
        if (grounded && !activeGrapple)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;

        TextStuff();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        Keyboard keyboard = Keyboard.current;

        horizontalInput = 0f;
        verticalInput = 0f;

        if (keyboard != null)
        {
            // Movimiento WASD / Flechas
            if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed) verticalInput += 1f;
            if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed) verticalInput -= 1f;
            if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) horizontalInput += 1f;
            if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) horizontalInput -= 1f;

            // Salto
            if (keyboard.spaceKey.isPressed && readyToJump && grounded)
            {
                readyToJump = false;
                Jump();
                Invoke(nameof(ResetJump), jumpCooldown);
            }
        }
    }

    private void StateHandler()
    {
        if (freeze)
        {
            state = MovementState.freeze;
            rb.linearVelocity = Vector3.zero;
        }
        else if (activeGrapple)
        {
            state = MovementState.grappling;
        }
        else if (grounded)
        {
            state = MovementState.walking;
        }
        else
        {
            state = MovementState.air;
        }
    }

    private void MovePlayer()
    {
        // Solo bloqueamos la entrada manual si el jugador está congelado (apuntando/preparando el disparo)
        if (freeze) return;

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Movimiento en suelo
        if (grounded && !activeGrapple)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        // Movimiento mientras usas el gancho (permite corregir la trayectoria en el aire)
        else if (activeGrapple)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
        // Movimiento en el aire normal
        else
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        // Permitimos que la velocidad supere el límite base mientras el gancho está activo para no frenar el impulso
        if (activeGrapple) return;

        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    #region Grappling Mechanics

    public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
    {
        activeGrapple = true;

        velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
        Invoke(nameof(SetVelocity), 0.1f);

        Invoke(nameof(ResetRestrictions), 3f);
    }

    private void SetVelocity()
    {
        enableMovementOnNextTouch = true;
        rb.linearVelocity = velocityToSet;

        if (cam != null) cam.DoFov(95f);
    }

    public void ResetRestrictions()
    {
        activeGrapple = false;
        if (cam != null) cam.DoFov(85f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (enableMovementOnNextTouch)
        {
            enableMovementOnNextTouch = false;
            ResetRestrictions();

            Grappling grapplingScript = GetComponent<Grappling>();
            if (grapplingScript != null) grapplingScript.StopGrapple();
        }
    }

    public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    {
        float gravity = Physics.gravity.y;
        float displacementY = endPoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity)
            + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

        return velocityXZ + velocityY;
    }

    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = grounded ? Color.green : Color.red;
        Vector3 checkPos = groundCheck != null ? groundCheck.position : transform.position - new Vector3(0, 1f, 0);
        Gizmos.DrawWireSphere(checkPos, groundDistance);
    }

    #region Text & Debugging

    public TextMeshProUGUI text_speed;
    public TextMeshProUGUI text_mode;

    private void TextStuff()
    {
        if (text_speed == null || text_mode == null) return;

        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        text_speed.SetText("Speed: " + Round(flatVel.magnitude, 1) + " / " + Round(moveSpeed, 1));
        text_mode.SetText(state.ToString());
    }

    public static float Round(float value, int digits)
    {
        float mult = Mathf.Pow(10.0f, (float)digits);
        return Mathf.Round(value * mult) / mult;
    }

    #endregion
}
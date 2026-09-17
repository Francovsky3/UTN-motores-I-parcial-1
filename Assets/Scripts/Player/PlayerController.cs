using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    Vector3 direction;
    Vector2 input;
    GroundCheck groundCheck;

    [Header("Camera")]
    [SerializeField] Transform cam;

    [Header("Movement vars")]
    [SerializeField] float gravityMultiplier;
    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;

    [Header("Rotation vars")]
    [SerializeField] float smoothTime;
    float currentVelocity;
    float velocity;
    float gravity = -9.81f;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        groundCheck = GetComponentInChildren<GroundCheck>();
    }

    void Update()
    {
        ApplyGravity();
        var dir = ApplyRotation();
        ApplyMovement(dir);
    }

    void ApplyGravity()
    {
        if(CheckGrounded() && velocity < 0f) velocity = -1.0f;
        else
        {
            velocity += gravity * gravityMultiplier * Time.deltaTime;
        }
        direction.y = velocity;
    }

    Vector3 ApplyRotation()
    {
        if(input.sqrMagnitude == 0) return Vector3.zero;
        var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        return moveDir;
    }

    void ApplyMovement(Vector3 moveDir)
    {
        characterController.Move(speed * Time.deltaTime * new Vector3 (moveDir.x, direction.y, moveDir.z).normalized);
    }

    public void Move(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        direction = new Vector3(input.x, 0f, input.y);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(!context.started) return;
        if(!CheckGrounded()) return;
        velocity += jumpSpeed;
    }

    bool CheckGrounded()
    {
        return groundCheck.Ground;
    }
}

using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerHitHandlerCC : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Animator animator;
    [SerializeField] private MonoBehaviour movementScript;

    [Header("Configuración del golpe")]
    [SerializeField] private string carTag = "Car";
    [SerializeField] private string hitTriggerName = "Hit";
    [SerializeField] private string isHitBoolName = "isHit";
    [SerializeField] private float recoveryTime = 1.5f; 

    [Header("Empuje (Knockback)")]
    [SerializeField] private float knockbackSpeed = 10f;
    [SerializeField] private float knockbackDrag = 8f;
    [SerializeField] private float gravity = -20f;

    private CharacterController controller;
    private bool isHit = false;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        if (animator == null) animator = GetComponentInChildren<Animator>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!isHit && hit.gameObject.CompareTag(carTag))
        {
            Vector3 pushDir = (transform.position - hit.transform.position).normalized;
            StartCoroutine(HandleHitRoutine(pushDir));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isHit && other.CompareTag(carTag))
        {
            Vector3 pushDir = (transform.position - other.transform.position).normalized;
            StartCoroutine(HandleHitRoutine(pushDir));
        }
    }

    private IEnumerator HandleHitRoutine(Vector3 pushDirection)
    {
        isHit = true;

        Vector3 lookDir = -pushDirection;
        lookDir.y = 0f;
        if (lookDir.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(lookDir);
        }

        animator.SetBool(isHitBoolName, true);
        animator.SetTrigger(hitTriggerName);

        if (movementScript != null)
            movementScript.enabled = false;

        pushDirection.y = 0f;
        pushDirection.Normalize();
        Vector3 horizontalVelocity = pushDirection * knockbackSpeed;
        float verticalVelocity = 4f;

        float timer = 0f;
        while (timer < recoveryTime)
        {
            timer += Time.deltaTime;

            horizontalVelocity = Vector3.MoveTowards(horizontalVelocity, Vector3.zero, knockbackDrag * Time.deltaTime);

            if (controller.isGrounded && verticalVelocity < 0f)
                verticalVelocity = -2f;
            else
                verticalVelocity += gravity * Time.deltaTime;

            Vector3 finalMove = (horizontalVelocity + Vector3.up * verticalVelocity) * Time.deltaTime;
            controller.Move(finalMove);

            yield return null;
        }

        animator.SetBool(isHitBoolName, false);

        if (movementScript != null)
            movementScript.enabled = true;

        isHit = false;
    }
}



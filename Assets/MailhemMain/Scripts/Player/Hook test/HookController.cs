using UnityEngine;
using UnityEngine.InputSystem;

public class HookController : MonoBehaviour
{
    [Header("Hook")]
    [SerializeField] private Transform hookPoint;
    [SerializeField] private float hookRange = 30f;

    [Header("Package Pull")]
    [SerializeField] private float pullSpeed = 12f;
    [SerializeField] private float collectDistance = 1.2f;

    [Header("Detection")]
    [SerializeField] private LayerMask hookableLayers;
    [SerializeField] private float rayRadius = 0.6f;

    [Header("Player")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float playerRotationSpeed = 10f;

    [Header("Throw")]
    [SerializeField] private float throwForce = 15f;

    private Camera playerCamera;

    // Paquete que está siendo atraído
    private Rigidbody pulledPackage;
    private Collider pulledPackageCollider;

    // Paquete que el jugador está llevando
    private Rigidbody carriedPackage;
    private Collider carriedPackageCollider;

    // Escala mundial original del paquete
    private Vector3 carriedPackageOriginalScale;

    private bool carryingPackage = false;


    private void Start()
    {
        playerCamera = Camera.main;
    }


    private void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)

        {
            // Si ya lleva un paquete, lo lanza
            if (carryingPackage)
            {
                ThrowPackage();
            }
            // Si no lleva nada, dispara el gancho
            else
            {
                FireHook();
            }
        }
    }


    private void FixedUpdate()
    {
        PullPackage();
    }


    // =========================================================
    // DISPARAR GANCHO
    // =========================================================

    private void FireHook()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));


        Debug.DrawRay(ray.origin, ray.direction * hookRange, Color.red, 2f);

        if (Physics.SphereCast(ray, rayRadius, out RaycastHit hit, hookRange, hookableLayers))

        {
            Debug.Log("Gancho alcanzó: " + hit.collider.gameObject.name);

            Rigidbody targetRb = hit.collider.GetComponent<Rigidbody>();


            if (targetRb != null)
            {
                pulledPackage = targetRb;
                pulledPackageCollider = hit.collider;

                Debug.Log("ATRAYENDO PAQUETE");
            }

        }
        else
        {
            Debug.Log("El gancho no encontró ningún objetivo.");
        }
    }


    // =========================================================
    // ATRAER PAQUETE
    // =========================================================

    private void PullPackage()
    {
        if (pulledPackage == null)
            return;

        // Girar el jugador hacia el paquete
        Vector3 lookDirection = pulledPackage.position - playerTransform.position;

        lookDirection.y = 0f;

        if (lookDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, playerRotationSpeed * Time.fixedDeltaTime);

        }

        // Dirección hacia el HookPoint
        Vector3 direction = hookPoint.position - pulledPackage.position;

        float distance = direction.magnitude;

        // El paquete llegó al jugador
        if (distance <= collectDistance)
        {
            StartCarryingPackage();
            return;
        }

        direction.Normalize();

        // Atraer el paquete
        pulledPackage.MovePosition(pulledPackage.position + direction * pullSpeed * Time.fixedDeltaTime);

    }


    // =========================================================
    // RECOGER PAQUETE
    // =========================================================

    private void StartCarryingPackage()
    {
        carryingPackage = true;

        carriedPackage = pulledPackage;
        carriedPackageCollider = pulledPackageCollider;

        // Guardar la escala mundial original
        carriedPackageOriginalScale = carriedPackage.transform.lossyScale;

        // Detener completamente la física
        carriedPackage.linearVelocity = Vector3.zero;
        carriedPackage.angularVelocity = Vector3.zero;

        // Desactivar física
        carriedPackage.isKinematic = true;

        // Evitar que el paquete choque con el jugador
        if (carriedPackageCollider != null)
        {
            carriedPackageCollider.enabled = false;
        }

        // Convertirlo en hijo del HookPoint
        carriedPackage.transform.SetParent(hookPoint, true);

        // Colocarlo exactamente en el HookPoint
        carriedPackage.transform.position = hookPoint.position;

        carriedPackage.transform.rotation = hookPoint.rotation;


        // Mantener el mismo tamaño mundial
        SetWorldScale(carriedPackage.transform, carriedPackageOriginalScale);

        // Limpiar referencias de atracción
        pulledPackage = null;
        pulledPackageCollider = null;

        Debug.Log("Paquete recogido.");
    }


    // =========================================================
    // LANZAR PAQUETE
    // =========================================================

    private void ThrowPackage()
    {
        if (carriedPackage == null)
            return;

        // Sacar el paquete del HookPoint
        carriedPackage.transform.SetParent(null, true);


        // Restaurar exactamente su escala original
        carriedPackage.transform.localScale = carriedPackageOriginalScale;

        // Reactivar collider
        if (carriedPackageCollider != null)
        {
            carriedPackageCollider.enabled = true;
        }

        // Reactivar física
        carriedPackage.isKinematic = false;

        // Lanzar hacia adelante según la orientación del personaje

        Vector3 throwDirection =
            playerTransform.forward;

        carriedPackage.linearVelocity =
            throwDirection * throwForce;

        Debug.Log("Paquete lanzado hacia adelante.");

        // Limpiar referencias
        carriedPackage = null;
        carriedPackageCollider = null;

        carryingPackage = false;
    }


    // =========================================================
    // MANTENER ESCALA MUNDIAL
    // =========================================================

    private void SetWorldScale(Transform target, Vector3 worldScale)

    {
        Vector3 parentScale = target.parent != null ? target.parent.lossyScale : Vector3.one;


        target.localScale = new Vector3(worldScale.x / parentScale.x, worldScale.y / parentScale.y, worldScale.z / parentScale.z);


    }
}

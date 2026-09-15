using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThrowablePackage : MonoBehaviour
{
    private Rigidbody rb;
    private Collider col;
    private bool isGrabbed = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    public void OnGrabbed(Transform attachPoint)
    {
        isGrabbed = true;
        rb.isKinematic = true;
        if (col != null) col.enabled = false;

        transform.SetParent(attachPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void OnThrown(Vector3 direction, float force)
    {
        isGrabbed = false;
        transform.SetParent(null);

        // Reactivamos físicas y gravedad
        rb.isKinematic = false;
        rb.useGravity = true;

        if (col != null) col.enabled = true;

        // Limpiamos velocidades residuales acumuladas
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Dirección normalizada hacia el objetivo apuntado
        Vector3 launchDirection = direction.normalized;

        // Aplicamos el impulso directo hacia el punto exacto de la mira
        rb.AddForce(launchDirection * force, ForceMode.VelocityChange);
    }

    public bool IsGrabbed() => isGrabbed;
}
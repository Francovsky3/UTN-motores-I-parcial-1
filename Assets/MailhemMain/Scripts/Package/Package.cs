using UnityEngine;

public class Package : MonoBehaviour
{
    Rigidbody rb;
    MeshCollider meshCollider;

    [Header("Audio del Paquete (Bloque C)")]
    [SerializeField] private AudioClip[] pickupClips;
    [SerializeField] private AudioClip[] throwClips; 

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        meshCollider = GetComponent<MeshCollider>();
    }

    public void AttachToPlayer(Transform newPos)
    {
        PlayerGeneral player = FindAnyObjectByType<PlayerGeneral>();

    
        if (player != null && pickupClips != null && pickupClips.Length > 0)
        {
            player.SFXManager.PlaySFX(pickupClips);
        }
        else if (player == null)
        {
            Debug.LogError("Package: No se encontr� ning�n objeto con el script PlayerGeneral en la escena.");
        }

        meshCollider.enabled = false;
        rb.isKinematic = true;
        transform.parent = newPos;
        transform.position = newPos.position;
        transform.Rotate(Vector3.forward * 90f);
    }

    public void GetThrown(Transform newPos, float force)
    {

        PlayerGeneral player = FindAnyObjectByType<PlayerGeneral>();

        if (player != null && throwClips != null && throwClips.Length > 0)
        {
            player.SFXManager.PlaySFX(throwClips);
        }
        // ---------------------------------------

        meshCollider.enabled = true;
        rb.isKinematic = false;
        transform.parent = null;
        transform.position = newPos.position;
        rb.linearVelocity = force * Vector3.forward;
    }
}

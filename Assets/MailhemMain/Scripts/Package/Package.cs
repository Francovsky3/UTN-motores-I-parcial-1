using UnityEngine;

public class Package : MonoBehaviour
{
    Rigidbody rb;
    MeshCollider meshCollider;
    float pickupTimer;
    [SerializeField] float maxTimer = 2f;
    bool pickupable = true;

    [Header("Audio del Paquete (Bloque C)")]
    [SerializeField] private AudioClip[] pickupClips;
    [SerializeField] private AudioClip[] throwClips; 

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        meshCollider = GetComponent<MeshCollider>();
    }

    void Update()
    {
        if(pickupTimer > 0)
        {
            pickupTimer -= Time.deltaTime;
        }
        if(pickupTimer <= 0)
        {
            pickupTimer = 0;
            pickupable = true;
        }
    }

    public bool AttachToPlayer(Transform newPos)
    {
        if (!pickupable) return false;
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
        return true;
    }

    public void GetThrown(Transform newPos, float force)
    {

        PlayerGeneral player = FindAnyObjectByType<PlayerGeneral>();

        if (player != null && throwClips != null && throwClips.Length > 0)
        {
            player.SFXManager.PlaySFX(throwClips);
        }
        // ---------------------------------------
        pickupable = false;
        meshCollider.enabled = true;
        rb.isKinematic = false;
        transform.parent = null;
        transform.position = newPos.position;
        rb.linearVelocity = force * Vector3.forward;
    }
}

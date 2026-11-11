using UnityEngine;

public class Package : MonoBehaviour
{
    Rigidbody rb;
    MeshCollider meshCollider;
    float pickupTimer;
    [SerializeField] float maxTimer = 2f;
    bool pickupable = true;
    bool countdown = false;

    [Header("Audio del Paquete (Bloque C)")]
    [SerializeField] private AudioSource audioSource;
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
        if(pickupTimer <= 0 && countdown)
        {
            countdown = false;
            pickupTimer = 0;
            pickupable = true;
        }
    }

    public bool AttachToPlayer(Transform newPos)
    {
        if (!pickupable) return false;

        if (audioSource != null && pickupClips != null && pickupClips.Length > 0)
        {
            AudioClip clipToPlay = pickupClips[Random.Range(0, pickupClips.Length)];
            audioSource.PlayOneShot(clipToPlay);
        }
        else
        {
            Debug.LogWarning("Package: Falta asignar el AudioSource o los pickupClips en el Inspector.");
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
            player.PlayerAudioManager.PlaySFX(throwClips);
        }
 
        pickupable = false;
        countdown = true;
        pickupTimer = maxTimer;
        meshCollider.enabled = true;
        rb.isKinematic = false;
        transform.parent = null;
        transform.position = newPos.position;
        rb.linearVelocity = force * Vector3.forward;
    }
}

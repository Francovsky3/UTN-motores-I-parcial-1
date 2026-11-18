using UnityEngine;

public class Package : MonoBehaviour
{
    Rigidbody rb;
    BoxCollider boxCollider;
    float pickupTimer;
    [SerializeField] float maxTimer = 2f;
    bool pickupable = true;
    bool countdown = false;

    [Header("Package SFX")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] AudioClip[] pickupClips;
    [SerializeField] AudioClip[] throwClips; 
    
    [Header("Voice Clips")]
    [SerializeField] AudioClip pickupVoice;
    public delegate void Packs();
    public event Packs PickedUp;
    public event Packs Dropped;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
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

        // Reproducir voz del jugador
        PlayerGeneral player = FindAnyObjectByType<PlayerGeneral>();
        if (player != null && pickupVoice != null)
        {
            player.PlayerAudioManager.PlayVoice(pickupVoice);
        }

        boxCollider.enabled = false;
        rb.isKinematic = true;
        transform.parent = newPos;
        transform.position = newPos.position;
        transform.Rotate(Vector3.forward * 90f);
        PickedUp();
        return true;
    }

    public void GetThrown(Transform newPos, float force)
    {
        pickupable = false;
        countdown = true;
        pickupTimer = maxTimer;
        rb.isKinematic = false;
        transform.parent = null;
        transform.position = newPos.position;
        boxCollider.enabled = true;
        rb.linearVelocity = force * Camera.main.transform.forward;
        Dropped();
    }
}

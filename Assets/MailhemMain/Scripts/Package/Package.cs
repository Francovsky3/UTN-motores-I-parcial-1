using UnityEngine;
using UnityEngine.Audio;

public class Package : MonoBehaviour
{
    Rigidbody rb;
    MeshCollider meshCollider;
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

        // Comprobación y reproducción con depuración
        if (audioSource != null && pickupClips != null && pickupClips.Length > 0)
        {
            AudioClip clipToPlay = pickupClips[Random.Range(0, pickupClips.Length)];
            audioSource.PlayOneShot(clipToPlay);
            Debug.Log("Éxito: Reproduciendo audio de la caja -> " + clipToPlay.name);
        }
        else
        {
            Debug.LogWarning("Fallo de audio: El AudioSource es nulo o 'pickupClips' está vacío en el Inspector.");
        }

        // Reproducir voz del jugador
        PlayerGeneral player = FindAnyObjectByType<PlayerGeneral>();
        if (player != null && pickupVoice != null)
        {
            player.PlayerAudioManager.PlayVoice(pickupVoice);
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
        // ---------------------------------------
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

using UnityEngine;

public class MailboxScript : MonoBehaviour
{
    [SerializeField] AudioClip sonidoEntrega;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioManager manager;
    [SerializeField] AudioClip playerVoice;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogWarning("Falta el componente AudioSource en el buz�n.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Packages"))
        {
            if (sonidoEntrega != null && audioSource != null)
            {
                audioSource.PlayOneShot(sonidoEntrega);
            }

            manager.PlayVoice(playerVoice);

            Destroy(other.gameObject);
        }
    }
}
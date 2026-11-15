using UnityEngine;

public class Mailbox : MonoBehaviour
{
    [SerializeField] AudioClip sonidoEntrega;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioManager manager;
    [SerializeField] AudioClip playerVoice;
    GameObject package;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogWarning("Falta el componente AudioSource en el buz�n.");
        }
    }

    public void SetPackage(GameObject pack)
    {
        package = pack;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == package)
        {
            if (sonidoEntrega != null && audioSource != null)
            {
                audioSource.PlayOneShot(sonidoEntrega);
            }

            manager.PlayVoice(playerVoice);

            Destroy(other.gameObject);
            package = null;
        }
    }
}
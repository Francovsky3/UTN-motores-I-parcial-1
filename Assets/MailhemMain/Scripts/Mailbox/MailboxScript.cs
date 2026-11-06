using UnityEngine;

public class MailboxScript : MonoBehaviour
{
    [SerializeField] private AudioClip sonidoEntrega;
    private AudioSource audioSource;

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
            Debug.Log("�Paquete entregado con �xito!");

            if (sonidoEntrega != null && audioSource != null)
            {
                audioSource.PlayOneShot(sonidoEntrega);
            }

            Destroy(other.gameObject);
        }
    }
}
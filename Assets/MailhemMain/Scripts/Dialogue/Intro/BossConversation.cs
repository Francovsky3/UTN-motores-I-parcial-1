using UnityEngine;

public class ConversacionTrigger : MonoBehaviour
{
    [SerializeField] AudioSource bossDialogue;
    [SerializeField] PlayerGeneral player;
    [SerializeField] AudioClip playerClip;

    private bool activado = false;
    private bool esperandoRespuesta = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !activado)
        {
            activado = true;

            // Reproduce el primer diálogo
            bossDialogue.Play();

            esperandoRespuesta = true;
        }
    }

    private void Update()
    {
        if (esperandoRespuesta && !bossDialogue.isPlaying)
        {
            // Reproduce el diálogo del segundo personaje
            player.PlayerAudioManager.PlayVoice(playerClip);

            esperandoRespuesta = false;
        }
    }
}


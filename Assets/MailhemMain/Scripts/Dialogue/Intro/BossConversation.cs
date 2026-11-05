using System;
using UnityEngine;
using System.Collections;
[RequireComponent(typeof(BoxCollider))]
public class BossConversation : MonoBehaviour
{
    public AudioSource bossDialogue;
    public AudioSource playerDialogue;
    private bool dentro;

    private void OnTriggerEnter(Collider otro)
    {
        if (CompareTag("Player") && dentro != true)
        {
            dentro = true;
        }
        if (!otro.CompareTag("Player") || dentro) return;
        dentro = true;
        if (dentro == true)
        {
            bossDialogue.Play();
            if (bossDialogue.isPlaying == false)
            {
                playerDialogue.Play();
            }
            //new WaitForSeconds(bossDialogue.clip.length);
        }
    }

    /*IEnumerator IniciarConversacion()
    {
        // Habla el personaje 1
        bossDialogue.Play();

        yield return new WaitForSeconds(
            bossDialogue.clip.length
        );

        // Responde el personaje 2
        playerDialogue.Play();
    }*/
}


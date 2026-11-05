using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class BossDialogue : MonoBehaviour
{
    public AudioSource bossDialogue;
    private bool dentro;

    private void OnTriggerEnter(Collider otro)
    {

        if (!otro.CompareTag("Player") || dentro) return;
        dentro = true;
        if (dentro == true)
        {
            bossDialogue.Play();
        }

    }

}

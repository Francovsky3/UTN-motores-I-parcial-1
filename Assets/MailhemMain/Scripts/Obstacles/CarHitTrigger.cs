using UnityEngine;

public class CarHitTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip[] hitClips; 
    private bool yaChoco = false;

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Player") && !yaChoco)
        {
           
            PlayerSFX playerSFX = other.GetComponentInChildren<PlayerSFX>();

            if (playerSFX != null && hitClips != null && hitClips.Length > 0)
            {
                yaChoco = true;

                playerSFX.PlaySFX(hitClips);

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            yaChoco = false;
        }
    }
}

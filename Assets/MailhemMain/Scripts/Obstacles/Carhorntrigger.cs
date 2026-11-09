using UnityEngine;

public class CarHornTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip[] hornClips;

    private bool yaSono = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !yaSono)
        {
         
            AudioManager playerSFX = other.GetComponentInChildren<AudioManager>();

            if (playerSFX != null && hornClips != null && hornClips.Length > 0)
            {
                yaSono = true;
                playerSFX.PlaySFX(hornClips);
                Debug.Log("CarHornTrigger: Se reprodujo la bocina desde el propio auto.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            yaSono = false;
        }
    }
}

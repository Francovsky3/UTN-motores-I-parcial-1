using System.Collections;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    AudioClip lastClip;

    public void PlaySFX(AudioClip[] playClips)
    {
        if (playClips == null || playClips.Length == 0)
        {
            Debug.LogWarning("No se pasaron clips de audio a PlaySFX.");
            return;
        }

        if (playClips.Length == 1)
        {
            audioSource.clip = playClips[0];
            lastClip = playClips[0];
            Debug.Log("Sonó sin random (Solo había un clip disponible)");
        }
        
       else
        {
            AudioClip selectedClip;
            int intentos = 0;

            do
            {
                int selectedIndex = Random.Range(0, playClips.Length);
                selectedClip = playClips[selectedIndex];
                intentos++;
            }
   
            while (selectedClip == lastClip && intentos < 10);

            audioSource.clip = selectedClip;
            lastClip = selectedClip;
            Debug.Log("Sonó con random");
        }

        audioSource.Play();
    }
}

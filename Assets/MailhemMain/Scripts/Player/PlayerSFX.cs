using System.Collections;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    AudioClip[] clips;
    AudioClip lastClip;

    //recieves the clips of the event that produces sound, if there are multiple, select at random except if the clip is the same as the last one played
    public void PlaySFX(AudioClip[] playClips)
    {
        clips = playClips;
        if (clips.Length == 0)
        {
            audioSource.clip = clips[0];
            Debug.Log("Sono sin random");
        }
        else
        {
            StartCoroutine(ChooseSFX());
            Debug.Log("Sono con random");
        }
        audioSource.Play();
    }

    IEnumerator ChooseSFX()
    {
        int selectedIndex;
            do
            {
                selectedIndex = Random.Range(0, clips.Length);
                audioSource.clip = clips[selectedIndex];
            }
            while(clips[selectedIndex] == lastClip);
            lastClip = clips[selectedIndex];

            yield return null;
    }
}

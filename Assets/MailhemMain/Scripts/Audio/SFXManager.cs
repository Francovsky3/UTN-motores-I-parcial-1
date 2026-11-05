using System.Collections;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    AudioClip lastClip;
    AudioClip[] givenClips;

    public void PlaySFX(AudioClip[] playClips)
    {
        if (playClips == null || playClips.Length == 0 || audioSource.isPlaying) return;

        if (playClips.Length == 1)
        {
            audioSource.clip = playClips[0];
            lastClip = playClips[0];
        }
        
       else
        {
            givenClips = playClips;
            StartCoroutine(ChooseClip());
        }

        audioSource.Play();
    }

    IEnumerator ChooseClip()
    {
        int selectedIndex;
        do
        {
            selectedIndex = Random.Range(0, givenClips.Length);
        }
        while (givenClips[selectedIndex] == lastClip);
        
        audioSource.clip = givenClips[selectedIndex];
        lastClip = givenClips[selectedIndex];
        yield return null;
    }
}

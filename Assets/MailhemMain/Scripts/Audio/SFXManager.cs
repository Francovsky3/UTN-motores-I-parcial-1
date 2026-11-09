using System.Collections;
using UnityEngine;

/// <summary>
/// Manages all audio, can be used for any entity
/// </summary>
public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource voiceSource;
    AudioClip lastClip;
    AudioClip[] sfxClips;

    public void PlaySFX(AudioClip[] playClips)
    {
        if (playClips == null || playClips.Length == 0 || sfxSource.isPlaying) return;

        if (playClips.Length == 1)
        {
            sfxSource.clip = playClips[0];
            lastClip = playClips[0];
        }
        
       else
        {
            sfxClips = playClips;
            StartCoroutine(ChooseClip());
        }

        sfxSource.Play();
    }

    public void PlayVoice(AudioClip voice)
    {
        voiceSource.clip = voice;
        voiceSource.Play();
    }

    IEnumerator ChooseClip()
    {
        int selectedIndex;
        do
        {
            selectedIndex = Random.Range(0, sfxClips.Length);
        }
        while (sfxClips[selectedIndex] == lastClip);
        
        sfxSource.clip = sfxClips[selectedIndex];
        lastClip = sfxClips[selectedIndex];
        yield return null;
    }
}

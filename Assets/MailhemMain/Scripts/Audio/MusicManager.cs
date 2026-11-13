using System.Collections;
using UnityEngine;

/// <summary>
/// Manages the game's music, fading in and out different sources
/// </summary>
public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip baseMusic;
    [SerializeField] AudioClip guitarClip;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource guitarSource;


    void Awake()
    {
        musicSource.clip = baseMusic;
        guitarSource.clip = guitarClip;
        musicSource.Play();
        guitarSource.Play();
    }

    public void FadeGuitar(float volume, float duration)
    {
        StartCoroutine(Fade(guitarSource, volume, duration));
    }

    //Fades in or out the target source, can even let 
    IEnumerator Fade(AudioSource source, float targetVolume, float duration)
    {
        float startVolume = source.volume;
        float currentTime = 0f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, targetVolume, currentTime/duration);
            yield return null;
        }
    }
}

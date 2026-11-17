using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject config;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioClip clickSound;

    public void PlayClickSound()
    {
        if (sfxSource != null && clickSound != null)
        {
            sfxSource.PlayOneShot(clickSound);
        }
    }

    void OnEnable()
    {
        musicSource.Play();
        float musicVol = 1;
        StartCoroutine(FadeOut(musicVol));
    }

    public void OnButtonPressed(string sceneName)
    {
        PlayClickSound();
        float musicVol = 0;
        StartCoroutine(FadeOut(musicVol));
        SceneManager.LoadScene(sceneName);
    }

    public void OnConfigButtonPressed()
    {
        PlayClickSound();
        config.SetActive(true);
    }

    IEnumerator FadeOut(float value)
    {
        float startVolume = musicSource.volume;
        float currentTime = 0f;

        while (currentTime < 1)
        {
            currentTime += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, value, currentTime/1);
            yield return null;
        }
    }
}

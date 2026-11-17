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

    public void OnButtonPressed(string sceneName)
    {
        PlayClickSound();
        StartCoroutine(FadeOut());
        SceneManager.LoadScene(sceneName);
    }

    public void OnConfigButtonPressed()
    {
        PlayClickSound();
        config.SetActive(true);
    }

    IEnumerator FadeOut()
    {
        float startVolume = musicSource.volume;
        float currentTime = 0f;

        while (currentTime < 1)
        {
            currentTime += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, 0, currentTime/1);
            yield return null;
        }
    }
}

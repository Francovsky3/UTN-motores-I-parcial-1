using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject config;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clickSound;

    public void PlayClickSound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }

    public void OnButtonPressed(string sceneName)
    {
        PlayClickSound();
        SceneManager.LoadScene(sceneName);
    }

    public void OnConfigButtonPressed()
    {
        PlayClickSound();
        config.SetActive(true);
    }
}

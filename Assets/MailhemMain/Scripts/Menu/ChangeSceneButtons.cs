using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private AudioSource menuAudioSource;
    [SerializeField] private AudioClip buttonClickClip;

    void Start()
    {
        if (menuAudioSource == null)
        {
            menuAudioSource = GetComponent<AudioSource>();
        }
    }

    void FixedUpdate()
    {
        Keyboard keyboard = Keyboard.current;
        if (Keyboard.current != null && Keyboard.current.escapeKey.isPressed)
        {
            MoveToMainMenu();
        }
    }

    public void MoveToMainMenu()
    {
        PlayButtonSound();
        SceneManager.LoadScene("MainMenu");
    }

    public void MoveToHookTest()
    {
        PlayButtonSound();
        SceneManager.LoadScene("HookTest");
    }

    public void MoveToCarTest()
    {
        PlayButtonSound();
        SceneManager.LoadScene("CarTest");
    }

    public void MoveToPlayerTest()
    {
        PlayButtonSound();
        SceneManager.LoadScene("PlayerTest");
    }

    public void MoveToLevelTest()
    {
        PlayButtonSound();
        SceneManager.LoadScene("Level 1");
    }

    private void PlayButtonSound()
    {
        if (menuAudioSource != null && buttonClickClip != null)
        {
            menuAudioSource.PlayOneShot(buttonClickClip);
        }
    }
}

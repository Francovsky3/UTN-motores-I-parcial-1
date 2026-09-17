using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    void FixedUpdate()
    {
        Keyboard keyboard = Keyboard.current;
        if (Keyboard.current != null && Keyboard.current.escapeKey.isPressed)
        {
            MoveToMainMenu();
        }
        /*if (Keyboard.current != null && Keyboard.current.digit4Key.isPressed)
        {
            MoveToCarTest();
        }
        if (Keyboard.current != null && Keyboard.current.digit5Key.isPressed)
        {
            MoveToPlayerTest();
        }
        if (Keyboard.current != null && Keyboard.current.digit6Key.isPressed)
        {
            MoveToLevelTest();
        }*/
    }
    public void MoveToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void MoveToHookTest()
    {
        SceneManager.LoadScene("HookTest");
    }

    public void MoveToCarTest()
    {
        SceneManager.LoadScene("CarTest");
    }

    public void MoveToPlayerTest()
    {

        SceneManager.LoadScene("PlayerTest");
    }

    public void MoveToLevelTest()
    {
        SceneManager.LoadScene("Level 1");
    }
}

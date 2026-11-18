using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public delegate void Pause();

    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject HUD;

    public void OnOptionsButtonPressed()
    {
        optionsMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnCloseMenuButtonPressed()
    {
        HUD.SetActive(true);
        Time.timeScale = 1;
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnMainMenuButtonPressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenMenu()
    {
        HUD.SetActive(false);
        Time.timeScale = 0;
        gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
    }
}

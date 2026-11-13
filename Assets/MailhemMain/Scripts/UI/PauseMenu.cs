using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject optionsMenu;

    public void OnOptionsButtonPressed()
    {
        optionsMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnCloseMenuButtonPressed()
    {
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnMainMenuButtonPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenMenu()
    {
        gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
    }
}

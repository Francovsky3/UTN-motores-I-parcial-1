using UnityEngine;

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
    }
    public void OpenMenu()
    {
        gameObject.SetActive(true);
    }
}

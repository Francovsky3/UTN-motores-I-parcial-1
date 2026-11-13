using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnLevelButtonPressed()
    {
        SceneManager.LoadScene("Level 1");
    }
}

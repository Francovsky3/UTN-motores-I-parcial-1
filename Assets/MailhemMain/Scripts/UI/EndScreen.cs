using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public void OnMenuButtonPressed()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void OnGameButtonPressed()
    {
        SceneManager.LoadScene("Level 1");
    }
}

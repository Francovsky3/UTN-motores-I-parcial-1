using TMPro;
using UnityEngine;

public class UITimer : MonoBehaviour
{
    [SerializeField] LevelTimer timer;
    [SerializeField] TextMeshProUGUI uiText;
    int minutes;
    int seconds;

    void Update()
    {
        minutes = Mathf.FloorToInt(timer.timer / 60);
        seconds = Mathf.FloorToInt(timer.timer % 60);
        uiText.SetText(minutes + ":" + seconds); 
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] PackageManager spawner;
    [SerializeField] Mailbox[] mailboxes;

    public delegate void GameEvent();
    public static event GameEvent StartEvent;
    public static event GameEvent EndEvent;

    void OnEnable()
    {
        LevelTimer.TimeOut += Lose;
        EventTrigger.Triggered += EventStarted;
        EventTrigger.End += EventEnded;
        PackageManager.AllDelivered += Win;
    }

    void Start()
    {
        spawner.SpawnPackages(mailboxes);
    }

    void Lose()
    {
        SceneManager.LoadScene("Lose Screen");
    }

    void Win()
    {
        
    }

    void EventStarted()
    {
        StartEvent();
    }

    void EventEnded()
    {
        EndEvent();
    }

}

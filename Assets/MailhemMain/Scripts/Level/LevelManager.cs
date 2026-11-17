using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] PackageSpawner spawner;
    [SerializeField] Mailbox[] mailboxes;

    public delegate void GameEvent();
    public static event GameEvent StartEvent;
    public static event GameEvent EndEvent;

    void OnEnable()
    {
        LevelTimer.TimeOut += Lose;
        EventTrigger.Triggered += EventStarted;
        EventTrigger.End += EventEnded;
    }

    void Start()
    {
        spawner.SpawnPackages(mailboxes);
    }

    void Lose()
    {
        SceneManager.LoadScene("Lose Screen");
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

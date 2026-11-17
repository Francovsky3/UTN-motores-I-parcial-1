using UnityEngine;

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
    }

    void Start()
    {
        spawner.SpawnPackages(mailboxes);
    }

    void Lose()
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

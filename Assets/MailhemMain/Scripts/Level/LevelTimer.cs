using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    [Tooltip("The amount of time of the level in seconds")]
    [SerializeField] float time;
    [SerializeField] bool countdown;
    public float timer => time;
    public delegate void Timer();
    public static event Timer TimeOut;

    void Awake()
    {
        LevelManager.EndEvent += ResumeCountdown;
        LevelManager.StartEvent += PauseCountdown;
    }

    void Update()
    {
        if(countdown)
        {
            time -= Time.deltaTime;
        }
        if(time <= 0)
        {
            TimeOut();
        }
    }

    void PauseCountdown()
    {
        countdown = false;
    }

    void ResumeCountdown()
    {
        countdown = true;
    }
}

using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    public delegate void Trigger();
    public static event Trigger Triggered;
    void OnTriggerEnter(Collider other)
    {
        Triggered();
        enabled = false;
    }
}

using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    public delegate void Trigger();
    public static event Trigger Triggered;
    public static event Trigger End;
    void OnTriggerEnter(Collider other)
    {
        Triggered();
    }
    void OnTriggerExit(Collider other)
    {
        End();
        enabled = false;
    }
}

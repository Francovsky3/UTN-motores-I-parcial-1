using UnityEngine;

public class VoiceTrigger : MonoBehaviour
{
    [SerializeField] AudioManager manager;
    [SerializeField] AudioClip voiceClip;
    void OnTriggerEnter(Collider other)
    {
        manager.PlayVoice(voiceClip);
        enabled = false;
    }
}

using UnityEngine;

public class VoiceTrigger : MonoBehaviour
{
    [SerializeField] AudioManager manager;
    [SerializeField] AudioClip voiceClip;
    bool canPlay = true;
    void OnTriggerEnter(Collider other)
    {
        if(canPlay)
        {
            manager.PlayVoice(voiceClip);
            canPlay = false;
        }
    }
}

using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            MusicManager manager = other.GetComponent<MusicManager>();
            if (manager.musicState == MusicManager.MusicState.pack) return;
            else if(manager.musicState == MusicManager.MusicState.interior) manager.musicState = MusicManager.MusicState.exterior;
            else manager.musicState = MusicManager.MusicState.interior;
        }
    }
}

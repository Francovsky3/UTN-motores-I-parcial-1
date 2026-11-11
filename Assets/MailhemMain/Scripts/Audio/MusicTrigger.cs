using UnityEngine;

public class MusicZone : MonoBehaviour
{
    float guitarLowVolume = 0f;
    float guitarHighVolume = 1f;
    [SerializeField] float fadeDuration;
    [SerializeField] MusicManager manager;
    [SerializeField] bool inside = true;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && !inside)
        {
            manager.FadeGuitar(guitarLowVolume, fadeDuration);
            inside = true;
        }
        else if(other.gameObject.CompareTag("Player") && inside)
        {
            manager.FadeGuitar(guitarHighVolume, fadeDuration);
            inside = false;
        }
    }
}

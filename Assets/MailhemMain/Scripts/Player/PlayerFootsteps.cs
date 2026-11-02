using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] footstepClips;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayFootstepSound()
    {
        if (footstepClips.Length == 0) return;
        int randomIndex = Random.Range(0, footstepClips.Length);
        audioSource.clip = footstepClips[randomIndex];
        audioSource.pitch = Random.Range(0.85f, 1.15f);
        audioSource.volume = Random.Range(0.8f, 1.0f);
        audioSource.Play();
    }
}

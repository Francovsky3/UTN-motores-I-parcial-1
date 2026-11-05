using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    [SerializeField] AudioClip[] walkingClips;
    [SerializeField] AudioClip[] jumpingClips;
    [SerializeField] AudioClip[] hookClips;
    [SerializeField] AudioClip[] throwClips;

    public AudioClip[] WalkingClips => walkingClips;
    public AudioClip[] JumpingClips => jumpingClips;
    public AudioClip[] HookClips => hookClips;
    public AudioClip[] ThrowClips => throwClips;
}

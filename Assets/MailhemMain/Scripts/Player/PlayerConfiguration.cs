using UnityEngine;

/// <summary>
/// Holds all player variables for reference
/// </summary>
public class PlayerConfiguration : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] float moveSpeed;
    public float MoveSpeed => moveSpeed;

    [SerializeField] float smoothTime;
    public float SmoothTime => smoothTime;

    [SerializeField] float gravity;
    public float Gravity => gravity;
    [SerializeField] float gravityMultiplier;
    public float GravityMultiplier => gravityMultiplier;
    [SerializeField] float jumpForce;
    public float JumpForce => jumpForce;
}

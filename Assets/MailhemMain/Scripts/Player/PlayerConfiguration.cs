using UnityEngine;

/// <summary>
/// Holds all player variables for reference
/// </summary>
public class PlayerConfiguration : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed;
    public float MoveSpeed => moveSpeed;

    [SerializeField] float smoothTime;
    public float SmoothTime => smoothTime;

    [SerializeField] float gravity;
    public float Gravity => gravity;
    [Tooltip("Multiplies the gravity while the player is jumping")]
    [SerializeField] float gravityMultiplier;
    public float GravityMultiplier => gravityMultiplier;
    [SerializeField] float jumpForce;
    public float JumpForce => jumpForce;
    [Header("Packages")]
    [SerializeField] float throwForce;
    public float ThrowForce => throwForce;
}

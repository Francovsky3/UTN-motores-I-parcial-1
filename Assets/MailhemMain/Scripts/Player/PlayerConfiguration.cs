using UnityEngine;

/// <summary>
/// Holds all player variables for reference
/// </summary>
public class PlayerConfiguration : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed;
    public float MoveSpeed => moveSpeed;

    [Tooltip("The horizontal rotation speed")]
    [SerializeField] float smoothTime;
    public float SmoothTime => smoothTime;

    [SerializeField] float gravity;
    public float Gravity => gravity;

    [Tooltip("Multiplies the gravity while the player is jumping")]
    [SerializeField] float gravityMultiplier;
    public float GravityMultiplier => gravityMultiplier;

    [SerializeField] float jumpForce;
    public float JumpForce => jumpForce;

    [SerializeField] float maxFallSpeed;
    public float MaxFallSpeed => maxFallSpeed;

    [Tooltip("The rate at which horizontal speed is reduced while falling")]
    [SerializeField] float airResistance;
    public float AirResistance => airResistance;

    [Header("Packages")]
    [SerializeField] float throwForce;
    public float ThrowForce => throwForce;

    [Header("Hook movement")]
    [SerializeField] float hookForce;
    public float HookForce => hookForce;
    
    [Header("Aiming")]
    [SerializeField] float aimSensitivity;
    public float AimSensitivity => aimSensitivity;

    public void SetSensitivity(float value)
    {
        aimSensitivity = value;
    }
}

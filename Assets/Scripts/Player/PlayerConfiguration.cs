using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Holds the value of variables that determine player behaviour
/// </summary>
public class PlayerConfiguration : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] float moveSpeed;
    public float MOVESPEED => moveSpeed;
    [SerializeField] float turnSpeed;
    public float TURNSPEED => turnSpeed;
    [SerializeField] float jumpForce;
    public float JUMPFORCE => jumpForce;
    [SerializeField] float jumpGravity;
    public float JUMPGRAVITY => jumpGravity;
    [SerializeField] float gravity;
    public float GRAVITY => gravity;
}

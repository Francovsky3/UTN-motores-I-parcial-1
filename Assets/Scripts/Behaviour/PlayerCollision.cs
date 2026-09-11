using UnityEngine;

/// <summary>
/// Detects if the player is colliding with something
/// </summary>
public class PlayerCollision : MonoBehaviour
{
    [Header("Ground")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundDistance;
    [SerializeField] bool groundGizmos;

    public bool Ground => Physics.Raycast(groundCheck.position, -transform.up, -groundDistance, groundLayer);

    void OnDrawGizmos()
    {
        if(groundGizmos)
        {
            if(Ground) Gizmos.color = Color.green;
            else Gizmos.color = Color.red;

            Gizmos.DrawRay(groundCheck.position, -transform.up * -groundDistance);
        }
    }
}

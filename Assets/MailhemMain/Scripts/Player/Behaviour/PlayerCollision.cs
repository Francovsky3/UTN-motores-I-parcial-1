using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [Header("Ground")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundDistance;
    [SerializeField] bool groundGizmos;

    public bool Ground => Physics.Raycast(groundCheck.position, -transform.up, -groundDistance, groundLayer);

    private void OnDrawGizmos()
    {
        if(groundGizmos)
        {
            if(Ground) Gizmos.color = Color.green;
            else Gizmos.color = Color.red;

            Gizmos.DrawRay(groundCheck.position, -transform.up * -groundDistance);
        }
    }
}

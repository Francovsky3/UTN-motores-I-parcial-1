using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [Header("Ground")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundDistance;
    [SerializeField] bool groundGizmos;

    [Header("Packages")]
    [SerializeField] Transform packageCheck;
    [SerializeField] LayerMask packageLayer;
    [SerializeField] float packageRadius;
    [SerializeField] bool packageGizmos;

    public Collider[] Package => Physics.OverlapSphere(packageCheck.position, packageRadius, packageLayer);
    public bool Ground => Physics.Raycast(groundCheck.position, -transform.up, -groundDistance, groundLayer);

    private void OnDrawGizmos()
    {
        if(groundGizmos)
        {
            if(Ground) Gizmos.color = Color.green;
            else Gizmos.color = Color.red;

            Gizmos.DrawRay(groundCheck.position, -transform.up * -groundDistance);
        }
        if(packageGizmos)
        {
            if(Package.Length > 0) Gizmos.color = Color.green;
            else Gizmos.color = Color.red;
            
            Gizmos.DrawWireSphere(packageCheck.position, packageRadius);
        }
    }
}

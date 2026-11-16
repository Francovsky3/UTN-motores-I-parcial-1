using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [Header("Ground")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundRadius;
    [SerializeField] bool groundGizmos;

    [Header("Packages")]
    [SerializeField] Transform packageCheck;
    [SerializeField] LayerMask packageLayer;
    [SerializeField] float packageRadius;
    [SerializeField] bool packageGizmos;

    public Collider[] Package => Physics.OverlapSphere(packageCheck.position, packageRadius, packageLayer);
    public bool Ground => Physics.CheckSphere(groundCheck.position, groundRadius, groundLayer);

    private void OnDrawGizmos()
    {
        if(groundGizmos)
        {
            if(Ground) Gizmos.color = Color.green;
            else Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        }
        if(packageGizmos)
        {
            if(Package.Length > 0) Gizmos.color = Color.green;
            else Gizmos.color = Color.red;
            
            Gizmos.DrawWireSphere(packageCheck.position, packageRadius);
        }
    }
}

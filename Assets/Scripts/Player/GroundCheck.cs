using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [Header("Raycast")]
    [SerializeField] float distance;
    [SerializeField] LayerMask checkLayer;
    [SerializeField] bool checkGizmos;

    public bool Ground => Physics.Raycast(transform.position, -transform.up, distance, checkLayer);

    void OnDrawGizmos()
    {
        if(checkGizmos)
        {
            if(Ground) Gizmos.color = Color.green;
            else Gizmos.color = Color.red;

            Gizmos.DrawRay(transform.position, -transform.up * distance);
        }
    }
}

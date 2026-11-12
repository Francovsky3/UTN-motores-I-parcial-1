using UnityEngine;

public class HookController : MonoBehaviour
{
    [SerializeField] PlayerGeneral player;
    [Header("Hook")]
    [SerializeField] Transform hookPoint;
    public Transform HookPoint => hookPoint;

    [Header("Hook Configuration")]
    [Tooltip("The maximum distance the hook can extend to")]
    [SerializeField] float hookRange;
    [SerializeField] float grappleCooldown;

    [Tooltip("The radius of the hook detection sphere")]
    [SerializeField] float sphereRadius;

    public bool FireHook()
    {
        Ray ray = player.Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Debug.DrawRay(ray.origin, ray.direction * hookRange, Color.red, 2f);

        if (Physics.SphereCast(ray, sphereRadius, out RaycastHit hit))
        {
            if(hit.collider.gameObject.CompareTag("Hook Point"))
            {
                Debug.Log("Punto de gancho detectado");
                return true;
            }
            else if(hit.collider.gameObject.CompareTag("Packages"))
            {
                Debug.Log("Paquete detectado");
            }
        }
        else
        {
            Debug.Log("El gancho no encontró ningún objetivo.");
        }
        return false;
    }

    public void ReleaseHook()
    {
        
    }
}

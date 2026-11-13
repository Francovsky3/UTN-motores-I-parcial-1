using UnityEngine;

public class HookController : MonoBehaviour
{
    [SerializeField] PlayerGeneral player;
    [Header("Hook Configuration")]
    [Tooltip("The maximum distance the hook can extend to")]
    [SerializeField] float hookRange;
    [SerializeField] float grappleCooldown;

    [Tooltip("The radius of the hook detection sphere")]
    [SerializeField] float sphereRadius;
    [Tooltip("Force with which the packages are reeled back")]
    [SerializeField] float reelingForce;

    Rigidbody reeledPack;
    bool isReelingPack = false;

    Transform hookPoint;
    public Transform HookPoint => hookPoint;

    void Update()
    {
        if(isReelingPack)
        {
            Vector3 direction = reeledPack.transform.position - player.transform.position;
            reeledPack.linearVelocity = reelingForce * Time.deltaTime * direction;
        }
        if(player.Packages.HasPackage)
        {
            reeledPack = null;
            isReelingPack = false;
        }
    }

    public bool FireHook()
    {
        if(isReelingPack) return false;
        Ray ray = player.Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Debug.DrawRay(ray.origin, ray.direction * hookRange, Color.red, 2f);

        if (Physics.SphereCast(ray, sphereRadius, out RaycastHit hit))
        {
            if(hit.collider.gameObject.CompareTag("Hook Point"))
            {
                Debug.Log("Punto de gancho detectado");
                return true;
            }
            else if(hit.collider.gameObject.CompareTag("Packages") && !player.Packages.HasPackage)
            {
                Debug.Log("Paquete detectado");
                reeledPack = hit.collider.gameObject.GetComponent<Rigidbody>();
                isReelingPack = true;
                return false;
            }
            else if(hit.collider != null)
            {
                Debug.Log("Le pegaste a algo");
            }
        }
        else
        {
            Debug.Log("I swing again, and I missed");
        }
        return false;
    }

    public void ReleaseHook()
    {
        
    }
}

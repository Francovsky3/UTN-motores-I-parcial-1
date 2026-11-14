using UnityEngine;

public class HookController : MonoBehaviour
{
    [SerializeField] PlayerGeneral player;
    [Header("Hook Configuration")]
    [Tooltip("The maximum distance the hook can extend to")]
    [SerializeField] float hookRange;

    [Tooltip("The radius of the hook detection sphere")]
    [SerializeField] float sphereRadius;
    [Tooltip("Force with which the packages are reeled back")]
    [SerializeField] float reelingForce;
    
    [SerializeField] LayerMask hookableLayers;

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

        if (Physics.SphereCast(ray, sphereRadius, out RaycastHit hit, hookRange, hookableLayers))
        {
            if(hit.collider.gameObject.CompareTag("Hook Point"))
            {
                Debug.Log("Punto de gancho detectado");
                Debug.DrawRay(ray.origin, ray.direction * hookRange, Color.green, 2f);
                HookPoint.position = hit.collider.gameObject.transform.position;
                return true;
            }
            else if(hit.collider.gameObject.CompareTag("Packages"))
            {
                Debug.DrawRay(ray.origin, ray.direction * hookRange, Color.blue, 2f);
                Debug.Log("Paquete detectado");
                reeledPack = hit.collider.gameObject.GetComponent<Rigidbody>();
                isReelingPack = true;
                return false;
            }
            else if(hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("El player esta en el medio xd");
                Debug.DrawRay(ray.origin, ray.direction * hookRange, Color.yellow, 2f);
            }
        }
        else
        {
            Debug.Log("I swing again, and I missed");
            Debug.DrawRay(ray.origin, ray.direction * hookRange, Color.red, 2f);
        }
        return false;
    }

    public void ReleaseHook()
    {
        
    }
}

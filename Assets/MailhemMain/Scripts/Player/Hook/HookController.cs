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

    public Vector3 HookPoint {get; private set;}

    void Update()
    {
        if(isReelingPack && reeledPack == null)
        {
            isReelingPack = false;
        }
        if(isReelingPack)
        {
            Vector3 direction = (player.transform.position - reeledPack.transform.position ).normalized;
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
            player.PlayerAudioManager.PlaySFX(player.PlayerSFX.HookClips);
            if(hit.collider.gameObject.CompareTag("Hook Point"))
            {
                Debug.DrawRay(ray.origin, ray.direction * hookRange, Color.green, 2f);
                if(hit.collider.transform.position != null)
                {
                    HookPoint = hit.collider.transform.position;
                    return true;
                }
                
            }
            else if(hit.collider.gameObject.CompareTag("Packages"))
            {
                Debug.DrawRay(ray.origin, ray.direction * hookRange, Color.blue, 2f);
                reeledPack = hit.collider.gameObject.GetComponent<Rigidbody>();
                isReelingPack = true;
                return false;
            }
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * hookRange, Color.red, 2f);
        }
        return false;
    }
}

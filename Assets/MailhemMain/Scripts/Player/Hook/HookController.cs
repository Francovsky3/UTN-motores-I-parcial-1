using UnityEngine;

public class HookController : MonoBehaviour
{
    [SerializeField] PlayerGeneral player;
    [Header("Hook")]
    [SerializeField] Transform hookPoint;
    public Transform HookPoint => hookPoint;

    [Tooltip("The maximum distance the hook can extend to")]
    [SerializeField] float hookDistance;
    [SerializeField] float grappleCooldown;

    public void FireHook()
    {
        bool hit = Physics.Raycast(transform.position, player.Camera.transform.forward, hookDistance);
        Debug.Log(hit);
        Debug.DrawRay(transform.position, player.Camera.transform.forward * hookDistance);
    }
}

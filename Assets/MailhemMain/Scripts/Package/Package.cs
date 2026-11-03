using UnityEngine;

public class Package : MonoBehaviour
{
    Rigidbody rb;
    MeshCollider meshCollider;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        meshCollider = GetComponent<MeshCollider>();
    }
    public void AttachToPlayer(Transform newPos)
    {
        meshCollider.enabled = false;
        rb.isKinematic = true;
        transform.parent = newPos;
        transform.position = newPos.position;
        transform.Rotate(Vector3.forward * 90f);
    }

    public void GetThrown(Transform newPos, float force)
    {
        meshCollider.enabled = true;
        rb.isKinematic = false;
        transform.parent = null;
        transform.position = newPos.position;
        rb.linearVelocity = force * Vector3.forward;
    }
}

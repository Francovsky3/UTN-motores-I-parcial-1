
using UnityEngine;

public class CarAI : MonoBehaviour
{
    [Header("Ruta")]
    public Transform[] waypoints;
    public float speed = 10f;
    public float rotationSpeed = 5f;

    [Header("Detención al chocar")]
    public string playerTag = "Player";
    public float stopDuration = 2.5f;

    private int currentWaypoint = 0;
    private float stopTimer = 0f;
    private bool hasStoppedForHit = false;

    private void Update()
    {
        if (stopTimer > 0f)
        {
            stopTimer -= Time.deltaTime;
            if (stopTimer <= 0f)
            {
                stopTimer = 0f;
                hasStoppedForHit = false;
            }
            return;
        }

        if (waypoints == null || waypoints.Length == 0) return;

        Transform target = waypoints[currentWaypoint];

        Vector3 direction = target.position - transform.position;
        direction.y = 0;

        if (direction.sqrMagnitude > 0.001f)
        {
            transform.position += direction.normalized * speed * Time.deltaTime;

            Quaternion rotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                rotation,
                rotationSpeed * Time.deltaTime
            );
        }

        if (Vector3.Distance(transform.position, target.position) < 1f)
        {
            currentWaypoint++;

            if (currentWaypoint >= waypoints.Length)
            {
                Destroy(gameObject);
                return;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasStoppedForHit && collision.gameObject.CompareTag(playerTag))
        {
            hasStoppedForHit = true;
            stopTimer = stopDuration;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasStoppedForHit && other.CompareTag(playerTag))
        {
            hasStoppedForHit = true;
            stopTimer = stopDuration;
        }
    }
}
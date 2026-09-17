
using UnityEngine;

public class CarAI : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 7f;
    public float rotationSpeed = 5f;

    private int currentWaypoint = 0;

    private void Update()
    {
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
}
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPC_Controller : MonoBehaviour
{
    [Header("Destino")]
    //Si asignas un punto, irá a ese lugar. Si lo dejas vacío, elije puntos aleatorios
    public Transform targetPoint;
    public float wanderRadius = 25f;
    public float arrivalThreshold = 0.8f;

    [Header("Movimiento y pausas")]
    public float walkSpeed = 1.6f;
    [Range(0f, 1f)]
    public float pauseChance = 0.25f;
    public float minPauseTime = 1.5f;
    public float maxPauseTime = 4.0f;

    [Header("Animación")]
    public Animator animator;
    public string speedParameter = "Speed";
    public float animationDampTime = 0.1f;

    private NavMeshAgent agent;
    private float pauseTimer = 0f;
    private bool isPaused = false;
    private int speedHash;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        speedHash = Animator.StringToHash(speedParameter);
    }

    private void Start()
    {
        agent.speed = walkSpeed;
        agent.avoidancePriority = Random.Range(30, 70);

        if (targetPoint != null)
        {
            MoveToPoint(targetPoint.position);
        }
        else
        {
            PickNewDestination();
        }
    }

    private void Update()
    {
        if (isPaused)
        {
            agent.isStopped = true;
            pauseTimer -= Time.deltaTime;
            if (pauseTimer <= 0f)
            {
                isPaused = false;
                agent.isStopped = false;
                HandleArrival();
            }
        }
        else if (!agent.pathPending && agent.remainingDistance <= arrivalThreshold)
        {
            HandleArrival();
        }

        UpdateAnimation();
    }

    public void MoveToPoint(Vector3 destination)
    {
        isPaused = false;
        agent.isStopped = false;

        if (NavMesh.SamplePosition(destination, out NavMeshHit hit, 2.0f, agent.areaMask))
        {
            agent.SetDestination(hit.position);
        }
        else
        {
            agent.SetDestination(destination);
        }
    }

    private void HandleArrival()
    {
        if (targetPoint != null)
        {
            MoveToPoint(targetPoint.position);
            return;
        }

        if (Random.value < pauseChance)
        {
            isPaused = true;
            pauseTimer = Random.Range(minPauseTime, maxPauseTime);
        }
        else
        {
            PickNewDestination();
        }
    }

    private void PickNewDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, wanderRadius, agent.areaMask))
        {
            agent.SetDestination(hit.position);
        }
    }

    private void UpdateAnimation()
    {
        if (animator == null) return;

        float currentSpeed = isPaused ? 0f : agent.velocity.magnitude;
        animator.SetFloat(speedHash, currentSpeed, animationDampTime, Time.deltaTime);
    }
}

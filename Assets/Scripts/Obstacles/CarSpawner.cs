
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject carPrefab;
    public Transform spawnPoint;
    public Transform[] waypoints;

    public float respawnDelay = 2f;

    private GameObject currentCar;
    private float timer;

    private void Start()
    {
        SpawnCar();
    }

    private void Update()
    {
        if (currentCar == null)
        {
            timer += Time.deltaTime;

            if (timer >= respawnDelay)
            {
                SpawnCar();
                timer = 0f;
            }
        }
    }

    private void SpawnCar()
    {
        currentCar = Instantiate(
            carPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        CarAI carAI = currentCar.GetComponent<CarAI>();

        if (carAI != null)
        {
            carAI.waypoints = waypoints;
        }
    }
}
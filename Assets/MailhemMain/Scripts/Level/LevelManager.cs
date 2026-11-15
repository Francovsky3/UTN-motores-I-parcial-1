using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] PackageSpawner spawner;

    void Start()
    {
        spawner.SpawnPackages();
    }
}

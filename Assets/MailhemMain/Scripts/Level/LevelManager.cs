using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] PackageSpawner spawner;
    [SerializeField] Mailbox[] mailboxes;

    void Start()
    {
        spawner.SpawnPackages(mailboxes);
    }
}

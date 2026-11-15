using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] PackageSpawner spawner;
    [Header("References")]
    [SerializeField] Mailbox[] mailboxes;
}

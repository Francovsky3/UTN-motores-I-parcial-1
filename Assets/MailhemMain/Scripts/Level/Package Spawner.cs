using UnityEngine;

public class PackageSpawner : MonoBehaviour
{
    [SerializeField] GameObject packagePrefab;
    [Tooltip("The position of each package, make sure the number and order of positions coincides with their respective mailbox")]
    [SerializeField] Vector3[] packagesPosition;

    public void SpawnPackages(Mailbox[] mailboxes)
    {
        
    }
}

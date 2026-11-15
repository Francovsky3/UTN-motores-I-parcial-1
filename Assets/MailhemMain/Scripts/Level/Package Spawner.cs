using UnityEngine;

public class PackageSpawner : MonoBehaviour
{
    [SerializeField] GameObject packagePrefab;
    [Tooltip("The position of each package, make sure the number and order of positions coincides with the mailboxes")]
    [SerializeField] Vector3[] packagesPosition;
    [SerializeField] Mailbox[] mailboxes;

    public void SpawnPackages()
    {
        int index = 0;
        foreach(Mailbox mailbox in mailboxes)
        {
            GameObject package = Instantiate(packagePrefab, packagesPosition[index], Quaternion.identity);
            mailbox.SetPackage(package);
            index++;
        }
    }
}

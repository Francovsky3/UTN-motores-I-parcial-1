using UnityEngine;

public class PackageSpawner : MonoBehaviour
{
    [SerializeField] GameObject packagePrefab;
    [Tooltip("The position of each package, make sure the number and order of positions coincides with the mailboxes")]
    [SerializeField] Vector3[] packagesPosition;

    public void SpawnPackages(Mailbox[] mailboxes)
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

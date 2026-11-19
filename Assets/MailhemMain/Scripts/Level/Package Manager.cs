using UnityEngine;

public class PackageManager : MonoBehaviour
{
    [SerializeField] GameObject packagePrefab;
    [Tooltip("The position of each package, make sure the number and order of positions coincides with the mailboxes")]
    [SerializeField] Transform[] packagesPosition;

    [SerializeField] UICounter counter;
    public int MaxPackages {get; private set;}
    public int DeliveredPackages {get; private set;}
    public delegate void Packs();
    public static event Packs AllDelivered;

    void OnEnable()
    {
        Mailbox.Delivered += CheckPackagesDelivered;
    }

    public void SpawnPackages(Mailbox[] mailboxes)
    {
        int index = 0;
        foreach(Mailbox mailbox in mailboxes)
        {
            GameObject package = Instantiate(packagePrefab, packagesPosition[index].position, Quaternion.identity);
            mailbox.SetPackage(package);
            MaxPackages++;
            index++;
        }
        counter.SetTotalPacks(index +1);
    }

    void CheckPackagesDelivered()
    {
        DeliveredPackages++;
        if(DeliveredPackages == MaxPackages)
        {
            AllDelivered();
        }
    }
}

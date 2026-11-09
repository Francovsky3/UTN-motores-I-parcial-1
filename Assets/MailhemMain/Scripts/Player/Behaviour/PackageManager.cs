using UnityEngine;
/// <summary>
/// Holds the logic for player interaction with packages
/// </summary>
public class PackageManager
{
    Transform packageStorage;
    PlayerConfiguration config;
    Transform handPosition;
    public bool HasPackage {get; private set;}
    Package storedPack;
    public PackageManager(PlayerConfiguration configuration, Transform storage, Transform handPos)
    {
        config = configuration;
        packageStorage = storage;
        handPosition = handPos;
    }

    public bool StorePack(Package pack)
    {
        HasPackage = true;
        if (!pack.AttachToPlayer(packageStorage)) return false;
        storedPack = pack;
        return true;
    }

    public void ThrowPack()
    {
        storedPack.GetThrown(handPosition, config.ThrowForce);
        HasPackage = false;
        storedPack = null;
    }
}

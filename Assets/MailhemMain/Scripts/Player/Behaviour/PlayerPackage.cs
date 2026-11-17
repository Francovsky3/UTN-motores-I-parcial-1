using UnityEngine;
/// <summary>
/// Holds the logic for player interaction with packages
/// </summary>
public class PlayerPackage
{
    Transform packageStorage;
    PlayerConfiguration config;
    Transform handPosition;
    public bool HasPackage {get; private set;} = false;
    Package storedPack;
    public PlayerPackage(PlayerConfiguration configuration, Transform storage, Transform handPos)
    {
        config = configuration;
        packageStorage = storage;
        handPosition = handPos;
    }

    public bool StorePack(Package pack)
    {
        if (!pack.AttachToPlayer(packageStorage)) return false;
        HasPackage = true;
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

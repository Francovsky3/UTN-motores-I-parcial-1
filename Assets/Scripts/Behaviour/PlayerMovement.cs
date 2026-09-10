using UnityEngine;

public class PlayerMovement
{
    Rigidbody rigidbody;
    PlayerConfiguration playerConfig;

    public PlayerMovement(Rigidbody rb, PlayerConfiguration playerConfiguration)
    {
        rigidbody = rb;
        playerConfig = playerConfiguration;
    }

    public void VelocityMovement(Vector2 inputVec)
    {
        Vector3 movement = new Vector3(inputVec.x, 0f, inputVec.y) * playerConfig.MOVESPEED;
        rigidbody.linearVelocity = movement;
    }

    public void VelocityIdle()
    {
        rigidbody.linearVelocity = Vector3.zero;
    }
}

using Unity.Mathematics;
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
        Vector3 movement = new Vector3(inputVec.x, 0f, inputVec.y) * playerConfig.MOVESPEED * Time.deltaTime;
        rigidbody.linearVelocity = movement;
        PlayerRotation();
    }

    public void VelocityIdle()
    {
        rigidbody.linearVelocity = Vector3.zero;
    }

    public void PlayerRotation()
    {
        Vector3 velocityWithoutY = new Vector3(rigidbody.linearVelocity.x, 0f, rigidbody.linearVelocity.z);
        if(velocityWithoutY != Vector3.zero)
        {
            quaternion targetRotation = Quaternion.LookRotation(velocityWithoutY.normalized, Vector3.up);
            quaternion newRotation = Quaternion.Euler(0f, Quaternion.Lerp(rigidbody.rotation, targetRotation, playerConfig.TURNSPEED * Time.deltaTime).eulerAngles.y, 0f);
            rigidbody.MoveRotation(newRotation);
        }
    }
}

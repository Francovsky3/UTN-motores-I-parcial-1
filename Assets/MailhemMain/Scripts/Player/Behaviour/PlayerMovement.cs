using UnityEngine;


/// <summary>
/// Holds the logic for all player states
/// </summary>
public class PlayerMovement
{
    PlayerConfiguration config;
    CharacterController controller;
    public PlayerMovement(CharacterController playerController, PlayerConfiguration configuration)
    {
        config = configuration;
        controller = playerController;
    }

    //Base grounded movement
    public void VelocityMovement(Vector2 inputVec)
    {
        Vector3 movement = new Vector3(inputVec.x, 0f, inputVec.y) * config.MoveSpeed;
        controller.Move(movement);
    }

    //Idle
    public void VelocityIdle()
    {
        controller.Move(Vector3.zero);
    }
}

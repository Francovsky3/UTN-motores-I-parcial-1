using UnityEngine;


/// <summary>
/// Holds the logic for all player states
/// </summary>
public class PlayerMovement
{
    PlayerConfiguration config;
    CharacterController controller;
    Camera camera;
    Transform transform;
    float currentVelocity;

    public PlayerMovement(CharacterController playerController, PlayerConfiguration configuration, Camera cam, Transform trans)
    {
        config = configuration;
        controller = playerController;
        camera = cam;
        transform = trans;
    }

    //Base grounded movement
    public void VelocityMovement(Vector2 inputVec)
    {
        Vector3 movement = new Vector3(inputVec.x, 0f, inputVec.y) * config.MoveSpeed * Time.deltaTime;
        Vector3 dir = ApplyRotation(movement);
        controller.Move(dir);
    }

    //Idle
    public void VelocityIdle()
    {
        controller.Move(Vector3.zero);
    }

    //Rotation
    Vector3 ApplyRotation(Vector3 inputVec)
    {
        var targetAngle = Mathf.Atan2(inputVec.x, inputVec.z) * Mathf.Rad2Deg + camera.transform.eulerAngles.y;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, config.SmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        return moveDir;
    }
}

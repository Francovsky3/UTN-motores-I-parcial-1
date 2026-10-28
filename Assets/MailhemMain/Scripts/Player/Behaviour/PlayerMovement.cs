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
    public float VelocityY => controller.velocity.y;

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
        Vector3 movement = config.MoveSpeed * Time.deltaTime * new Vector3(inputVec.x, 0f, inputVec.y);
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

    public void VelocityJump()
    {
        controller.Move(config.JumpForce * Time.deltaTime * Vector3.up);
    }

    public void ApplyGravity()
    {
        controller.Move(new Vector3(0, config.Gravity * Time.deltaTime, 0));
    }

    //Apply gravity to the current y velocity
    public void ApplyJumpGravity()
    {
        float velocity = Mathf.Clamp(VelocityY - (config.Gravity * config.GravityMultiplier * Time.deltaTime), config.Gravity * config.GravityMultiplier, VelocityY);

        controller.Move(new Vector3(0, velocity, 0));
    }
}

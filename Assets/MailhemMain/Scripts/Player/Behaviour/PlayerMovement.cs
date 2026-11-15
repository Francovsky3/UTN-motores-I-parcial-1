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
    public Vector3 Velocity => controller.velocity;

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
        Vector3 dir = ApplyRotation(inputVec);
        controller.Move(config.MoveSpeed * Time.deltaTime * dir);
    }

    //Idle
    public void VelocityIdle()
    {
        controller.Move(Vector3.zero);
    }

    //Rotation
    Vector3 ApplyRotation(Vector2 inputVec)
    {
        var targetAngle = Mathf.Atan2(inputVec.x, inputVec.y) * Mathf.Rad2Deg + camera.transform.eulerAngles.y;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, config.SmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        return moveDir;
    }

    public void VelocityJump()
    {
        Vector3 velocity = new (Velocity.x, config.JumpForce, Velocity.z);
        controller.Move(velocity * Time.deltaTime);
    }

    public void ApplyGravity()
    {
        controller.Move(new Vector3(Velocity.x, config.Gravity, Velocity.z) * Time.deltaTime);
    }

    //Apply gravity to the current y velocity and allow movement
    public void JumpMovement(Vector2 inputVec)
    {
        float velocityY = Velocity.y + (config.Gravity * config.GravityMultiplier);
        Vector3 velocity = new Vector3(Velocity.x, velocityY, Velocity.z);

        controller.Move(velocity * Time.deltaTime);
    }

    public void HookedMovement(Vector3 targetPos)
    {
        Vector3 direction = (targetPos - transform.position).normalized;
        controller.Move(config.HookForce * Time.deltaTime * direction);
    }
}

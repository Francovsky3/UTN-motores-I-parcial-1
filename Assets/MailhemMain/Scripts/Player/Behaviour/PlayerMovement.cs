using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// Holds the logic for all player states
/// </summary>
public class PlayerMovement
{
    PlayerConfiguration config;
    CharacterController controller;
    Camera camera;
    Transform transform;
    Transform target;

    float currentVelocity;
    public Vector3 Velocity => controller.velocity;

    public PlayerMovement(CharacterController playerController, PlayerConfiguration configuration, Camera cam, Transform trans, Transform tag)
    {
        config = configuration;
        controller = playerController;
        camera = cam;
        transform = trans;
        target = tag;
    }

    public void VelocityIdle()
    {
        controller.Move(Vector3.zero);
    }

    public void VelocityMovement(Vector2 inputVec)
    {
        Vector3 dir = ApplyRotation(inputVec);
        Vector3 velocity = config.MoveSpeed * Time.deltaTime * dir;
        controller.Move(velocity);
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
        Vector3 velocity = new Vector3(Velocity.x, config.JumpForce, Velocity.z) * Time.deltaTime;
        controller.Move(velocity);
    }

    public void ApplyGravity()
    {
        float fallSpeedY = Mathf.Clamp(Velocity.y + (config.Gravity * config.GravityMultiplier), config.MaxFallSpeed, Velocity.y);
        Vector2 horizontalSpeed = ApplyAirResistance();
        Vector3 velocity = new Vector3(horizontalSpeed.x, fallSpeedY, horizontalSpeed.y) * Time.deltaTime;

        controller.Move(velocity);
    }

    public void JumpMovement()
    {
        float velocityY = Velocity.y + (config.Gravity * config.GravityMultiplier);
        Vector3 velocity = new Vector3(Velocity.x, velocityY, Velocity.z)* Time.deltaTime;

        controller.Move(velocity);
    }

    public void HookedMovement(Vector3 targetPos)
    {
        Vector3 dir = (targetPos - transform.position).normalized;
        Vector3 velocity = config.HookForce * Time.deltaTime * dir;
        controller.Move(velocity);
    }
    public void AimMovement()
    {
        float xRotation = 0;
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        xRotation -= Mathf.Clamp(mouseDelta.y, -90, 90);
        target.Rotate(new Vector3(xRotation, mouseDelta.x, 0f) * config.AimSensitivity);

        controller.Move(Velocity * Time.deltaTime);
    }

    public Vector2 ApplyAirResistance()
    {
        float fallSpeedX;
        float fallSpeedZ;
        switch (Velocity.x)
        {
            case <0:
                fallSpeedX = Mathf.Clamp(Velocity.x + config.AirResistance, Velocity.x, 0f);
                break;
            case >0:
                fallSpeedX = Mathf.Clamp(Velocity.x - config.AirResistance, 0f, Velocity.x);
                break;
            default:
                fallSpeedX = 0;
                break;
        }
        switch (Velocity.z)
        {
            case <0:
                fallSpeedZ = Mathf.Clamp(Velocity.z + config.AirResistance, Velocity.z, 0f);
                break;
            case >0:
                fallSpeedZ = Mathf.Clamp(Velocity.z - config.AirResistance, 0f, Velocity.z);
                break;
            default:
                fallSpeedZ = 0;
                break;
        }

        return new Vector2(fallSpeedX, fallSpeedZ);
    }
}

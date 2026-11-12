using UnityEngine;

/// <summary>
/// Processes the input recieved by the input manager for the current state to read
/// </summary>
public class InputProcessor
{
    public Vector2 InputVector {get; private set;}

    public Vector2 InputVectorNormal => InputVector.normalized;

    public float InputJump {get; private set;}

    public float InputFireHook {get; private set;}
    public float InputReleaseHook {get; private set;}
    public float InputThrow {get; private set;}

    public void ProcessInputVector(Vector2 value)
    {
        InputVector = value;
    }

    public void ProcessInputJump(float value)
    {
        InputJump = value;
    }

    public void ProcessInputFireHook(float value)
    {
        InputFireHook = value;
    }

    public void ProcessInputReleaseHook(float value)
    {
        InputReleaseHook = value;
    }

    public void ProcessInputThrow(float value)
    {
        InputThrow = value;
    }
}

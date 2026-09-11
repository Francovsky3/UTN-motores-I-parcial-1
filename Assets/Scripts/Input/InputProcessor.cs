using UnityEngine;

/// <summary>
/// Recieves player input and returns the input value
/// </summary>
public class InputProcessor
{
    public Vector2 InputVector {get; private set;}

    public Vector2 InputVectorNormal => InputVector.normalized;

    public float InputJump {get; private set;}

    public void ProcessInputVector(Vector2 value)
    {
        InputVector = value;
    }

    public void ProcessInputJump(float value)
    {
        InputJump = value;
    }
}

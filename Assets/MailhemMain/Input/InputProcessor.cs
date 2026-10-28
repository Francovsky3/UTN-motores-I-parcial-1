using UnityEngine;

/// <summary>
/// Processes the input recieved by the input manager for the current state to read
/// </summary>
public class InputProcessor
{
    public Vector2 InputVector {get; private set;}

    public Vector2 InputVectorNormal => InputVector.normalized;

    public void ProcessInputVector(Vector2 value)
    {
        InputVector = value;
    }
}

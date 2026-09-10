using UnityEngine;

public class InputProcessor
{
    public Vector2 InputVector {get; private set;}

    public Vector2 InputVectorNormal => InputVector.normalized;

    public void ProcessInputVector(Vector2 value)
    {
        InputVector = value;
    }
}

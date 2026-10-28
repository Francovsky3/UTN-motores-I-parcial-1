using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// Recieves the input data and gives it to the processor
/// </summary>
public class InputManager : MonoBehaviour
{
    [SerializeField] PlayerGeneral player;

    public void InputMovement(InputAction.CallbackContext context)
    {
        player.InputProcessor.ProcessInputVector(context.ReadValue<Vector2>());
    }

    public void InputJump(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            float time = (float)context.time;
            player.InputProcessor.ProcessesInputJump(time);
        }
    }
}

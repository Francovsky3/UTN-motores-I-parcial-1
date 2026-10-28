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
}

using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] PlayerGeneral player;

    public void InputMovement(InputAction.CallbackContext context)
    {
        player.InputProcessor.ProcessInputVector(context.ReadValue<Vector2>());
    }
}

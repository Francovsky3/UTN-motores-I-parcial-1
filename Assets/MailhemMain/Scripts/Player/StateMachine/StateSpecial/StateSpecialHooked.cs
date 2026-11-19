using UnityEngine;
using UnityEngine.InputSystem;

public class StateSpecialHooked : StateManager
{
    private const string IsHookedParam = "isHooked";
    private const string HookTriggerParam = "HookStart";

    public StateSpecialHooked(PlayerGeneral playerGeneral, StateMachine stateMachine, Animator anim)
        : base(playerGeneral, stateMachine, anim)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (animator != null)
        {
            animator.SetBool(IsHookedParam, true);
            animator.SetTrigger(HookTriggerParam);
        }
    }

    public override void Update()
    {
        base.Update();

        Player.Movement.HookedMovement(Player.Hook.HookPoint);

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            ExitHook();
            StateMachine.Change(Player.States.IdleState);
        }
    }

    private void ExitHook()
    {
        if (animator != null)
        {
            animator.SetBool(IsHookedParam, false);
            animator.ResetTrigger(HookTriggerParam);
        }
    }
}

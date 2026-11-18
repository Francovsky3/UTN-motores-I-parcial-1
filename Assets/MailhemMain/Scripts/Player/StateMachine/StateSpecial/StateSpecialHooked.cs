using UnityEngine;
using UnityEngine.InputSystem;

public class StateSpecialHooked : StateManager
{
    public StateSpecialHooked(PlayerGeneral playerGeneral, StateMachine stateMachine, Animator anim) : base(playerGeneral, stateMachine, anim)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        animator.SetBool("isHooked", true);
    }

    public override void Update()
    {
        base.Update();
        Player.Movement.HookedMovement(Player.Hook.HookPoint);
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            animator.SetBool("isHooked", false);
            
            StateMachine.Change(Player.States.IdleState);
            
        }
    }
}

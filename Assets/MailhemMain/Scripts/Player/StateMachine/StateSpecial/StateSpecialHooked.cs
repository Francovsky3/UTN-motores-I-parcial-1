using UnityEngine;

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
        Player.Movement.HookedMovement(Player.Hook.HookPoint.position);
        if(Player.InputProcessor.InputReleaseHook > Time.time)
        {
            animator.SetBool("isHooked", false);
            Player.InputProcessor.ProcessInputReleaseHook(0);
            StateMachine.Change(Player.States.IdleState);
        }
    }
}

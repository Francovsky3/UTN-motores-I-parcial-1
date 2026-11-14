using UnityEngine;
/// <summary>
/// Base class for all air states
/// </summary>
public class StateSuperAir : StateManager
{
    public StateSuperAir(PlayerGeneral playerGeneral, StateMachine stateMachine, Animator anim) : base(playerGeneral, stateMachine, anim)
    {
    }

    public override void Update()
    {
        base.Update();
        if(Player.InputProcessor.InputFireHook > Time.time)
        {
            bool hit = Player.Hook.FireHook();
            Player.InputProcessor.ProcessInputFireHook(0);
            if (hit)
            {
                StateMachine.Change(Player.States.HookedState);
                animator.SetBool("isInAir", false);
                animator.SetTrigger("isJumping");
            }
        }
    }
}

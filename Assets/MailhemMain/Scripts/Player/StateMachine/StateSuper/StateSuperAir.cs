using UnityEngine;
using UnityEngine.InputSystem;
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
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            bool hit = Player.Hook.FireHook();
            if (hit)
            {
                StateMachine.Change(Player.States.HookedState);
                animator.SetBool("isInAir", false);
                animator.SetTrigger("isJumping");
            }
        }
    }
}

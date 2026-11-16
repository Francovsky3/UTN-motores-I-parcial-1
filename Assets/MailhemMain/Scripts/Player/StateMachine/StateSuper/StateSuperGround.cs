using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// BAse class for all grounded states
/// </summary>
public class StateSuperGround : StateManager
{
    public StateSuperGround(PlayerGeneral playerGeneral, StateMachine stateMachine, Animator anim) : base(playerGeneral, stateMachine, anim)
    {
        
    }

    public override void Update()
    {
        base.Update();
        if(!Player.Collision.Ground)
        {
            StateMachine.Change(Player.States.InAirState);
        }
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            bool hit = Player.Hook.FireHook();
            if(hit)
            {
                StateMachine.Change(Player.States.HookedState);
                animator.SetBool("isIdle", false);
                animator.SetBool("isWalking", false);
            }
        }
    }
}

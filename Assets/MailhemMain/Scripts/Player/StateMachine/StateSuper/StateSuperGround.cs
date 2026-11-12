using UnityEngine;
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
        if(Player.InputProcessor.InputFireHook > Time.time)
        {
            Player.Hook.FireHook();
        }
    }
}

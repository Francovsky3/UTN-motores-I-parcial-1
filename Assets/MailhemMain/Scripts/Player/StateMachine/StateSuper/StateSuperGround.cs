using UnityEngine;
/// <summary>
/// BAse class for all grounded states
/// </summary>
public class StateSuperGround : StateManager
{
    public StateSuperGround(PlayerGeneral playerGeneral, StateMachine stateMachine) : base(playerGeneral, stateMachine)
    {
        
    }

    public override void Update()
    {
        base.Update();
        if(!Player.Collision.Ground)
        {
            StateMachine.Change(Player.States.InAirState);
        }
        if(Player.Collision.Ground && Player.InputProcessor.InputJump > Time.time)
        {
            Player.InputProcessor.ProcessesInputJump(0);
            StateMachine.Change(Player.States.JumpState);
        }
    }
}

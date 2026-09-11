using UnityEngine;

/// <summary>
/// PArent class of all player grounded states
/// </summary>
public class ParentStateGround : StateManager
{
    public ParentStateGround(PlayerGeneral player, StateMachine stateMachine) : base(player, stateMachine)
    {

    }

    public override void Update()
    {
        base.Update();
        if(!player.Collision.Ground)
        {
            stateMachine.Change(player.states.InAirState);
        }

        if(player.Collision.Ground && player.InputProcessor.InputJump > Time.time)
        {
            player.InputProcessor.ProcessInputJump(0);
            stateMachine.Change(player.states.JumpState);
        }
    }
}

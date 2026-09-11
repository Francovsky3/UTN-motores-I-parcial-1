using UnityEngine;

public class ChildStateIdle : ParentStateGround
{
    public ChildStateIdle(PlayerGeneral player, StateMachine stateMachine) : base(player, stateMachine)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        player.PlayerMovement.VelocityIdle();
    }

    public override void Update()
    {
        base.Update();
        if (player.InputProcessor.InputVector.magnitude > 0)
        {
            stateMachine.Change(player.states.MovementState);
        }
    }
}

using UnityEngine;
/// <summary>
/// State for when the player is doing nothing
/// </summary>
public class StateSubIdle : StateSuperGround
{
    public StateSubIdle(PlayerGeneral playerGeneral, StateMachine stateMachine) : base(playerGeneral, stateMachine)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        Player.Movement.VelocityIdle();
    }

    public override void Update()
    {
        base.Update();
        if(Player.InputProcessor.InputVector.magnitude > 0)
        {
            StateMachine.Change(Player.States.MoveState);
        }
        if(Player.Collision.Ground && Player.InputProcessor.InputJump > Time.time)
        {
            Player.InputProcessor.ProcessesInputJump(0);
            StateMachine.Change(Player.States.JumpState);
        }
    }
}

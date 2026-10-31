using UnityEngine;
/// <summary>
/// State for when the player is moving on the ground
/// </summary>
public class StateSubMovement : StateSuperGround
{
    public StateSubMovement(PlayerGeneral playerGeneral, StateMachine stateMachine) : base(playerGeneral, stateMachine)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        if(Player.InputProcessor.InputVector.magnitude == 0)
        {
            StateMachine.Change(Player.States.IdleState);
        }
        Player.Movement.VelocityMovement(Player.InputProcessor.InputVectorNormal);
        if(Player.Collision.Ground && Player.InputProcessor.InputJump > Time.time)
        {
            Player.InputProcessor.ProcessesInputJump(0);
            StateMachine.Change(Player.States.JumpState);
        }
    }
}

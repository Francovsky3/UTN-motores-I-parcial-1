using UnityEngine;
/// <summary>
/// State for when the player is doing nothing
/// </summary>
public class StateSubIdle : StateSuperGround
{
    private static readonly int isIdleHash = Animator.StringToHash("isIdle");

    public StateSubIdle(PlayerGeneral playerGeneral, StateMachine stateMachine, Animator anim) : base(playerGeneral, stateMachine, anim)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        animator.SetBool(isIdleHash, true);
        Player.Movement.VelocityIdle();
    }

    public override void Update()
    {
        base.Update();
        if(Player.InputProcessor.InputVector.magnitude > 0)
        {
            animator.SetBool("isIdle", false);
            StateMachine.Change(Player.States.MoveState);
        }
        if(Player.Collision.Ground && Player.InputProcessor.InputJump > Time.time)
        {
            animator.SetBool("isIdle", false);
            Player.InputProcessor.ProcessInputJump(0);
            StateMachine.Change(Player.States.JumpState);
        }
    }
}

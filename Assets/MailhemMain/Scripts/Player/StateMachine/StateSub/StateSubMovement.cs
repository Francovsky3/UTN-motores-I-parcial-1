using UnityEngine;
/// <summary>
/// State for when the player is moving on the ground
/// </summary>
public class StateSubMovement : StateSuperGround
{
    private static readonly int isWalkingHash = Animator.StringToHash("isWalking");

    public StateSubMovement(PlayerGeneral playerGeneral, StateMachine stateMachine, Animator anim) : base(playerGeneral, stateMachine, anim)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        animator.SetBool(isWalkingHash, true);
    }

    public override void Update()
    {
        base.Update();
        if(Player.InputProcessor.InputVector.magnitude == 0)
        {
            animator.SetBool("isWalking", false);
            StateMachine.Change(Player.States.IdleState);
        }
        Player.Movement.VelocityMovement(Player.InputProcessor.InputVectorNormal);
        if(Player.Collision.Ground && Player.InputProcessor.InputJump > Time.time)
        {
            animator.SetBool("isWalking", false);
            Player.InputProcessor.ProcessInputJump(0);
            StateMachine.Change(Player.States.JumpState);
        }
    }
}

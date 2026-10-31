using UnityEngine;


/// <summary>
/// State for player jumping
/// </summary>
public class StateSubJump : StateSuperAir
{
    private static readonly int isJumpingHash = Animator.StringToHash("isJumping");

    public StateSubJump(PlayerGeneral playerGeneral, StateMachine stateMachine, Animator anim) : base(playerGeneral, stateMachine, anim)
    {
    }

    public override void Enter()
    {
        base.Enter();
        animator.SetBool(isJumpingHash, true);
        Player.Movement.VelocityJump();
    }

    public override void Update()
    {
        base.Update();
        Player.Movement.JumpMovement(Player.InputProcessor.InputVectorNormal);
        if(Player.Movement.VelocityY <= 0)
        {
            animator.SetBool("isJumping", false);
            StateMachine.Change(Player.States.InAirState);
        }
    }
}

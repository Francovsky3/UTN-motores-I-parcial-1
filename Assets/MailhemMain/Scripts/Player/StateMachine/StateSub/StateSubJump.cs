using UnityEngine;


/// <summary>
/// State for player jumping
/// </summary>
public class StateSubJump : StateSuperAir
{
    private static readonly int isJumpingHash = Animator.StringToHash("isJumping");

    private float jumpTimer;
    private float minJumpDuration = 0.5f;

    public StateSubJump(PlayerGeneral playerGeneral, StateMachine stateMachine, Animator anim) : base(playerGeneral, stateMachine, anim)
    {
    }

    public override void Enter()
    {
        base.Enter();
        animator.SetTrigger(isJumpingHash);
        Player.Movement.VelocityJump();
        jumpTimer = 1.5f;
        Debug.Log("entrando a a saltar");
        Player.PlayerSFX.PlaySFX(Player.JumpingClips);
    }

    public override void Update()
    {
        base.Update();
        Player.Movement.JumpMovement(Player.InputProcessor.InputVectorNormal);
        jumpTimer += Time.deltaTime;
        if(jumpTimer >= minJumpDuration && Player.Movement.VelocityY <= 0)
        {
            StateMachine.Change(Player.States.InAirState);
        }
    }
}

using UnityEngine;

public class StateSubMovement : StateSuperGround
{
    private static readonly int isWalkingHash = Animator.StringToHash("isWalking");

 
    private float footstepTimer;
    private float timeBetweenSteps = 0.45f;

    public StateSubMovement(PlayerGeneral playerGeneral, StateMachine stateMachine, Animator anim) : base(playerGeneral, stateMachine, anim)
    {
       
    }

    public override void Enter()
    {
        base.Enter();
        animator.SetBool(isWalkingHash, true);
        footstepTimer = timeBetweenSteps;
    }

    public override void Update()
    {
        base.Update();
        if (Player.InputProcessor.InputVector.magnitude == 0)
        {
            animator.SetBool("isWalking", false);
            StateMachine.Change(Player.States.IdleState);
            return;
        }

        Player.Movement.VelocityMovement(Player.InputProcessor.InputVectorNormal);

        footstepTimer += Time.deltaTime;

 
        if (footstepTimer >= timeBetweenSteps)
        {
            Player.PlayerSFX.PlaySFX(Player.WalkingClips);

            footstepTimer = 0f;
        }

        if (Player.Collision.Ground && Player.InputProcessor.InputJump > Time.time)
        {
            animator.SetBool("isWalking", false);
            Player.InputProcessor.ProcessInputJump(0);
            StateMachine.Change(Player.States.JumpState);
        }
    }
}
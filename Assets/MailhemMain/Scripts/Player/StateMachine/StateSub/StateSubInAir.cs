using UnityEngine;

/// <summary>
/// When player is falling
/// </summary>
public class StateSubInAir : StateSuperAir
{
    private const string IsInAirParam = "isInAir";
    private const string IsFallingParam = "isFalling";

    private const float highFallThreshold = 7f; // distancia

    private float startFallY;
    private bool hasTriggeredHighFall;

    public StateSubInAir(PlayerGeneral playerGeneral, StateMachine stateMachine, Animator anim)
        : base(playerGeneral, stateMachine, anim)
    {
    }

    public override void Enter()
    {
        base.Enter();

        startFallY = Player.transform.position.y;
        hasTriggeredHighFall = false;

        if (animator != null)
        {
            animator.SetBool(IsInAirParam, true);
            animator.SetBool(IsFallingParam, false);
        }
    }

    public override void Update()
    {
        base.Update();
        Player.Movement.ApplyGravity();

        float currentFallDistance = startFallY - Player.transform.position.y;

        if (!hasTriggeredHighFall && currentFallDistance >= highFallThreshold)
        {
            hasTriggeredHighFall = true;
            if (animator != null)
            {
                animator.SetBool(IsFallingParam, true);
            }
        }

        if (Player.Collision.Ground)
        {
            if (animator != null)
            {
                animator.SetBool(IsFallingParam, false);
                animator.SetBool(IsInAirParam, false);
            }

            if (Player.InputProcessor.InputVectorNormal.magnitude == 0)
            {
                StateMachine.Change(Player.States.IdleState);
            }
            else
            {
                StateMachine.Change(Player.States.MoveState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        Player.Particles.Emit();
    }
}

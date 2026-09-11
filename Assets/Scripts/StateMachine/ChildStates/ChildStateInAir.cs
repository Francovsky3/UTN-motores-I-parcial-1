using UnityEngine;

/// <summary>
/// Player state for falling
/// </summary>
public class ChildStateInAir : ParentStateAir
{
    public ChildStateInAir(PlayerGeneral player, StateMachine stateMachine) : base(player, stateMachine){}

    public override void Update()
    {
        base.Update();
        if(player.Collision.Ground)
        {
            if(player.InputProcessor.InputVectorNormal.magnitude == 0)
            {
                stateMachine.Change(player.states.IdleState);
            }
            else
            {
                stateMachine.Change(player.states.MovementState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        Physics.gravity = Vector3.up * player.Config.GRAVITY;
    }
}

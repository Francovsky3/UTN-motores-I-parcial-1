using UnityEngine;

/// <summary>
/// Player state for jumping
/// </summary>
public class ChildStateJump : ParentStateAir
{
    public ChildStateJump(PlayerGeneral player, StateMachine stateMachine) : base(player, stateMachine){}

    public override void Enter()
    {
        base.Enter();
        player.Movement.VelocityJump();
        Physics.gravity = Vector3.up * player.Config.JUMPGRAVITY;
    }

    public override void Update()
    {
        base.Update();
        if(player.Movement.VelocityY < 0)
        {
            stateMachine.Change(player.states.InAirState);
        }
    }
}

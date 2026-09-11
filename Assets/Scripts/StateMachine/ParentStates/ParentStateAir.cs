using UnityEngine;

/// <summary>
/// PArent class of all player air states
/// </summary>
public class ParentStateAir : StateManager
{
    public ParentStateAir(PlayerGeneral player, StateMachine stateMachine) : base(player, stateMachine){}

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.Movement.VelocityMovementInAir(player.InputProcessor.InputVectorNormal);
    }
}

using UnityEngine;

public class ChildStateMovement : ParentStateGround
{
    public ChildStateMovement(PlayerGeneral player, StateMachine stateMachine) : base(player, stateMachine) {}

    public override void Update()
    {
        base.Update();
        if(player.InputProcessor.InputVector.magnitude == 0)
        {
            stateMachine.Change(player.states.IdleState);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.PlayerMovement.VelocityMovement(player.InputProcessor.InputVectorNormal);
    }
}

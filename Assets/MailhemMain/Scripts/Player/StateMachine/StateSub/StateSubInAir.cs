/// <summary>
/// When player is falling
/// </summary>
public class StateSubInAir : StateSuperAir
{
    public StateSubInAir(PlayerGeneral playerGeneral, StateMachine stateMachine) : base(playerGeneral, stateMachine)
    {
    }

    public override void Update()
    {
        base.Update();
        Player.Movement.ApplyGravity();
        if(Player.Collision.Ground)
        {
            if(Player.InputProcessor.InputVectorNormal.magnitude == 0)
            {
                StateMachine.Change(Player.States.IdleState);
            }
            else
            {
                StateMachine.Change(Player.States.MoveState);
            }
        }
    }
}

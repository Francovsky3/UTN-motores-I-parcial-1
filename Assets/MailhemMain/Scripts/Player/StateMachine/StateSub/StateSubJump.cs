/// <summary>
/// State for player jumping
/// </summary>
public class StateSubJump : StateSuperAir
{
    public StateSubJump(PlayerGeneral playerGeneral, StateMachine stateMachine) : base(playerGeneral, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.Movement.VelocityJump();
    }

    public override void Update()
    {
        base.Update();
        Player.Movement.JumpMovement(Player.InputProcessor.InputVectorNormal);
        if(Player.Movement.VelocityY <= 0)
        {
            StateMachine.Change(Player.States.InAirState);
        }
    }
}

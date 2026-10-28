/// <summary>
/// State for when the player is doing nothing
/// 
/// 
/// Lazy ass
/// </summary>
public class StateSubIdle : StateSuperGround
{
    public StateSubIdle(PlayerGeneral playerGeneral, StateMachine stateMachine) : base(playerGeneral, stateMachine)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        Player.Movement.VelocityIdle();
    }

    public override void Update()
    {
        base.Update();
        if(Player.InputProcessor.InputVector.magnitude > 0)
        {
            StateMachine.Change(Player.States.MoveState);
        }
    }
}

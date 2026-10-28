/// <summary>
/// Instances and contains all of the state machine's states for easy reference
/// </summary>
public class StateCollection
{
    public StateSubIdle IdleState {get; private set;}
    public StateSubMovement MoveState {get; private set;}
    public StateSubInAir InAirState {get; private set;}
    public StateSubJump JumpState {get; private set;}

    public StateCollection(PlayerGeneral player, StateMachine stateMachine)
    {
        IdleState = new StateSubIdle(player, stateMachine);
        MoveState = new StateSubMovement(player, stateMachine);
        InAirState = new StateSubInAir(player, stateMachine);
        JumpState = new StateSubJump(player, stateMachine);
    }
}

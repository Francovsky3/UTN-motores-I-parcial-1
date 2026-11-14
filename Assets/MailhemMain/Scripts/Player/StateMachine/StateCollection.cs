using UnityEngine;
/// <summary>
/// Instances and contains all of the state machine's states for easy reference
/// </summary>
public class StateCollection
{
    public StateSubIdle IdleState {get; private set;}
    public StateSubMovement MoveState {get; private set;}
    public StateSubInAir InAirState {get; private set;}
    public StateSubJump JumpState {get; private set;}
    public StateSpecialHooked HookedState {get; private set;}

    public StateCollection(PlayerGeneral player, StateMachine stateMachine, Animator animator)
    {
        IdleState = new StateSubIdle(player, stateMachine, animator);
        MoveState = new StateSubMovement(player, stateMachine, animator);
        InAirState = new StateSubInAir(player, stateMachine, animator);
        JumpState = new StateSubJump(player, stateMachine, animator);
        HookedState = new StateSpecialHooked(player, stateMachine, animator);
    }
}

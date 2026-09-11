using UnityEngine;

/// <summary>
/// Holds all player states instances for referencing
/// </summary>
public class StateCollection
{
    public ChildStateIdle IdleState {get; private set;}
    public ChildStateMovement MovementState {get; private set;}
    
    public ChildStateInAir InAirState {get; private set;}
    public ChildStateJump JumpState {get; private set;}

    public StateCollection(PlayerGeneral player, StateMachine stateMachine)
    {
        IdleState = new ChildStateIdle(player, stateMachine);
        MovementState = new ChildStateMovement(player, stateMachine);
        InAirState = new ChildStateInAir(player, stateMachine);
        JumpState = new ChildStateJump(player, stateMachine);
    }
}

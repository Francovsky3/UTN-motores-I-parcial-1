using UnityEngine;
using UnityEngine.Purchasing;

public class StateCollection
{
    public ChildStateIdle IdleState {get; private set;}

    public ChildStateMovement MovementState {get; private set;}

    public StateCollection(PlayerGeneral player, StateMachine stateMachine)
    {
        IdleState = new ChildStateIdle(player, stateMachine);
        MovementState = new ChildStateMovement(player, stateMachine);
    }
}

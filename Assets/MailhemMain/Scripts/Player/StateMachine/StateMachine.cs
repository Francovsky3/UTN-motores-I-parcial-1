using UnityEngine;
/// <summary>
/// The base of the player's state machine
/// </summary>
public class StateMachine
{
    public StateManager state {get; private set;}

    public void Init(StateManager _state)
    {
        state = _state;
        state.Enter();
    }

    public void Change(StateManager _state)
    {
        state.Exit();
        Init(_state);
    }
}

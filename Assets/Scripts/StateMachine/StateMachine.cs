using Unity.VisualScripting;
using UnityEngine;

public class StateMachine
{
    public StateManager State {get; private set;}

    public void Init(StateManager state)
    {
        State = state;
        State.Enter();
    }

    public void Change(StateManager state)
    {
        State.Exit();
        Init(state);
    }
}

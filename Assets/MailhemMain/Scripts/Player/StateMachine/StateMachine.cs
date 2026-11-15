/// <summary>
/// The base of the player's state machine
/// </summary>
public class StateMachine
{
    public StateManager State {get; private set;}

    public void Init(StateManager _state)
    {
        State = _state;
        State.Enter();
    }

    public void Change(StateManager _state)
    {
        State.Exit();
        Init(_state);
    }
}

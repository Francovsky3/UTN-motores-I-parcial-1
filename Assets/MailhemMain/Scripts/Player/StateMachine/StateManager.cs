/// <summary>
/// Executes the current state logic
/// </summary>
public class StateManager
{
    protected StateMachine StateMachine;
    protected PlayerGeneral Player;

    public StateManager(PlayerGeneral playerGeneral, StateMachine stateMachine)
    {
        Player = playerGeneral;
        StateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        
    }

    public virtual void Exit()
    {
        
    }

    public virtual void Update()
    {
        
    }

    public virtual void FixedUpdate()
    {
        
    }

    public virtual void LateUpdate()
    {
        
    }
}

using UnityEngine;

/// <summary>
/// Base class of all states
/// </summary>
public class StateManager
{
    protected StateMachine stateMachine;
    protected PlayerGeneral player;

    public StateManager(PlayerGeneral playerGeneral, StateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        player = playerGeneral;
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


using UnityEngine;

public class StateManager
{
    protected StateMachine STATEMACHINE;
    protected PlayerGeneral PLAYER;

    public StateManager(PlayerGeneral playerGeneral, StateMachine stateMachine)
    {
        STATEMACHINE = stateMachine;
        PLAYER = playerGeneral;
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


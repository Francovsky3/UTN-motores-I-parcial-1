using UnityEngine;


/// <summary>
/// Executes the current state logic
/// </summary>
public class StateManager
{
    protected StateMachine StateMachine;
    protected PlayerGeneral Player;
    protected Animator animator;

    public StateManager(PlayerGeneral playerGeneral, StateMachine stateMachine, Animator anim)
    {
        Player = playerGeneral;
        StateMachine = stateMachine;
        animator = anim;
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

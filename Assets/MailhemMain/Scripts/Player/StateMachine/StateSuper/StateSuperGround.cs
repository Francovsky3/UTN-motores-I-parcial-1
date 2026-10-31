using UnityEngine;
/// <summary>
/// BAse class for all grounded states
/// </summary>
public class StateSuperGround : StateManager
{
    public StateSuperGround(PlayerGeneral playerGeneral, StateMachine stateMachine) : base(playerGeneral, stateMachine)
    {
        
    }

    public override void Update()
    {
        base.Update();
        if(!Player.Collision.Ground)
        {
            StateMachine.Change(Player.States.InAirState);
        }
        
    }
}

using UnityEngine;
/// <summary>
/// Base class for all air states
/// </summary>
public class StateSuperAir : StateManager
{
    public StateSuperAir(PlayerGeneral playerGeneral, StateMachine stateMachine, Animator anim) : base(playerGeneral, stateMachine, anim)
    {
    }
}

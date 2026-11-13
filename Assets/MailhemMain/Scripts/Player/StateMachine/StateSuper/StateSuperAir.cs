using UnityEngine;
/// <summary>
/// Base class for all air states
/// </summary>
public class StateSuperAir : StateManager
{
    public StateSuperAir(PlayerGeneral playerGeneral, StateMachine stateMachine, Animator anim) : base(playerGeneral, stateMachine, anim)
    {
    }

    public override void Update()
    {
        base.Update();
        if(Player.InputProcessor.InputFireHook > Time.time)
        {
            Player.Hook.FireHook();
            Player.InputProcessor.ProcessInputFireHook(0);
        }
    }
}

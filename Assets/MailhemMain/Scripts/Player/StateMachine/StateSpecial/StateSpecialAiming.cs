using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateSpecialAiming : StateManager
{
    CinemachineCamera camera;
    public StateSpecialAiming(PlayerGeneral playerGeneral, StateMachine stateMachine, Animator anim, CinemachineCamera aimCam) : base(playerGeneral, stateMachine, anim)
    {
        camera = aimCam;
    }

    public override void Enter()
    {
        base.Enter();
        camera.Priority = 2;
        Time.timeScale = 0.3f;
    }

    public override void Update()
    {
        base.Update();
        Player.Movement.AimMovement();
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            if(Player.Hook.FireHook())
            {
                StateMachine.Change(Player.States.HookedState);
            }
        }
        if(Mouse.current.rightButton.wasReleasedThisFrame)
        {
            if(Player.Collision.Ground)
            {
                StateMachine.Change(Player.States.IdleState);
            }
            else
            {
                StateMachine.Change(Player.States.InAirState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        camera.Priority = 0;
        Time.timeScale = 1;
    }
}

using UnityEngine;


/// <summary>
/// Base class for all states
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
        if(Player.Collision.Package.Length > 0 && Player.Packages.HasPackage == false)
        {
            if (Player.Packages.StorePack(Player.Collision.Package[0].GetComponent<Package>()))
            {
                //Player.MusicManager.SwitchMusicState(MusicManager.MusicState.pack);
                Debug.Log("Agarrado");
            }
            
        }
        if(Player.InputProcessor.InputThrow > Time.time && Player.Packages.HasPackage == true)
        {
            Player.InputProcessor.ProcessInputThrow(0);
            Player.Packages.ThrowPack();
            //Player.MusicManager.SwitchMusicState(MusicManager.MusicState.exterior);
        }
    }

    public virtual void FixedUpdate()
    {
        
    }

    public virtual void LateUpdate()
    {
        
    }
}

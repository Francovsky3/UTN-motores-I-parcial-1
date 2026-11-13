using UnityEngine;

/// <summary>
/// The main script for the player, contains all components
/// </summary>
public class PlayerGeneral : MonoBehaviour
{
    public StateMachine StateMachine {get; private set;}
    public StateCollection States {get; private set;}
    public InputProcessor InputProcessor {get; private set;}
    public PlayerMovement Movement {get; private set;}
    public PackageManager Packages {get; private set;}
    public PlayerCollision Collision => collision;
    public HookController Hook => hook;
    public Camera Camera => playerCamera;
    public AudioManager PlayerAudioManager => audioManager;
    public PlayerSFX PlayerSFX => playerSFX;
    public MusicManager MusicManager => musicManager;

    [Header("Movement Components")]
    [SerializeField] CharacterController characterController;
    [SerializeField] PlayerConfiguration playerConfig;
    [SerializeField] Camera playerCamera;
    [SerializeField] Transform playerTrans;
    [SerializeField] Animator meshAnimator;

    [Header("Collision Components")]
    [SerializeField] PlayerCollision collision;

    [Header("Gameplay Components")]
    [SerializeField] HookController hook;
    [SerializeField] Transform packageStorage;
    [SerializeField] Transform handPosition;

    [Header("Audio")]
    [SerializeField] AudioManager audioManager;
    [SerializeField] PlayerSFX playerSFX;
    [SerializeField] MusicManager musicManager;

    private void Awake()
    {
        StateMachine = new StateMachine();
        States = new StateCollection(this, StateMachine, meshAnimator);
        InputProcessor = new InputProcessor();
        Movement = new PlayerMovement(characterController, playerConfig, playerCamera, playerTrans);
        Packages = new PackageManager(playerConfig, packageStorage, handPosition);
        Cursor.lockState = CursorLockMode.Locked;
    }

    //Initialize the state machine
    void Start()
    {
        StateMachine.Init(States.IdleState);
    }

    //Execute current state's Fixed Update
    void FixedUpdate()
    {
        StateMachine.state.FixedUpdate();
    }

    //Execute current state's Update
    void Update()
    {
        StateMachine.state.Update();
    }

    //Execute current state's Late Update
    void LateUpdate()
    {
        StateMachine.state.LateUpdate();
    }
}

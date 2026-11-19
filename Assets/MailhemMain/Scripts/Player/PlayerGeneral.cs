using TMPro;
using Unity.Cinemachine;
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
    public PlayerPackage Packages {get; private set;}
    public PlayerCollision Collision => collision;
    public HookController Hook => hook;
    public Camera Camera => playerCamera;
    public AudioManager PlayerAudioManager => audioManager;
    public PlayerSFX PlayerSFX => playerSFX;
    public MusicManager MusicManager => musicManager;
    public ParticleInstatiator Particles => particles;

    [Header("Movement Components")]
    [SerializeField] CharacterController characterController;
    [SerializeField] PlayerConfiguration playerConfig;
    [SerializeField] Camera playerCamera;
    [SerializeField] Transform playerTrans;
    [SerializeField] Animator meshAnimator;
    [SerializeField] CinemachineCamera aimCamera;

    [Header("Collision Components")]
    [SerializeField] PlayerCollision collision;

    [Header("Gameplay Components")]
    [SerializeField] HookController hook;
    [SerializeField] Transform packageStorage;
    [SerializeField] Transform handPosition;
    [SerializeField] Transform aimTarget;
    [SerializeField] ParticleInstatiator particles;

    [Header("Audio")]
    [SerializeField] AudioManager audioManager;
    [SerializeField] PlayerSFX playerSFX;
    [SerializeField] MusicManager musicManager;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI currentState;
    

    private void Awake()
    {
        StateMachine = new StateMachine();
        States = new StateCollection(this, StateMachine, meshAnimator, aimCamera);
        InputProcessor = new InputProcessor();
        Movement = new PlayerMovement(characterController, playerConfig, playerCamera, playerTrans, aimTarget);
        Packages = new PlayerPackage(playerConfig, packageStorage, handPosition);
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
        StateMachine.State.FixedUpdate();
    }

    //Execute current state's Update
    void Update()
    {
        StateMachine.State.Update();
        currentState.text = StateMachine.State.ToString();
    }

    //Execute current state's Late Update
    void LateUpdate()
    {
        StateMachine.State.LateUpdate();
    }
}

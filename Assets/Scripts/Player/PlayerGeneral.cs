using TMPro;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Calls the state machine methods
/// </summary>
public class PlayerGeneral : MonoBehaviour
{
    public StateMachine StateMachine {get; private set;}

    public StateCollection states {get; private set;}

    public InputProcessor InputProcessor {get; private set;}

    public PlayerMovement Movement {get; private set;}

    public PlayerCollision Collision => collision;

    public PlayerConfiguration Config => playerConfig;

    [Header("Components")]
    [SerializeField] Rigidbody Rigidbody;
    [SerializeField] PlayerConfiguration playerConfig;
    [SerializeField] PlayerCollision collision;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI CurrentState;

    void Awake()
    {
        StateMachine = new StateMachine();
        states = new StateCollection(this, StateMachine);
        InputProcessor = new InputProcessor();
        Movement = new PlayerMovement(Rigidbody, playerConfig);
    }

    void Start()
    {
        StateMachine.Init(states.IdleState);
    }

    void Update()
    {
        StateMachine.State.Update();
        CurrentState.text = StateMachine.State.ToString();
    }

    void FixedUpdate()
    {
        StateMachine.State.FixedUpdate();
    }

    void LateUpdate()
    {
        StateMachine.State.LateUpdate();
    }
}

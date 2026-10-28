using TMPro;
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

    [Header("Components")]
    [SerializeField] CharacterController characterController;
    [SerializeField] PlayerConfiguration playerConfig;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI currentState;

    private void Awake()
    {
        StateMachine = new StateMachine();
        States = new StateCollection(this, StateMachine);
        InputProcessor = new InputProcessor();
        Movement = new PlayerMovement(characterController, playerConfig);
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
        currentState.text = StateMachine.state.ToString();
    }

    //Execute current state's Late Update
    void LateUpdate()
    {
        StateMachine.state.LateUpdate();
    }
}

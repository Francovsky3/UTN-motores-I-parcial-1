using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGeneral : MonoBehaviour
{
    public StateMachine StateMachine {get; private set;}

    public StateCollection states {get; private set;}

    public InputProcessor InputProcessor {get; private set;}

    public PlayerMovement PlayerMovement {get; private set;}

    [Header("Components")]
    [SerializeField] Rigidbody Rigidbody;
    [SerializeField] PlayerConfiguration playerConfig;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI CurrentState;

    void Awake()
    {
        StateMachine = new StateMachine();
        states = new StateCollection(this, StateMachine);
        InputProcessor = new InputProcessor();
        PlayerMovement = new PlayerMovement(Rigidbody, playerConfig);
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

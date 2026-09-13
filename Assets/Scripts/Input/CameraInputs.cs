using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraInputs : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cmCamera;

    [SerializeField] private InputActionReference rightClickAction;

//HEAD
    [Header("Zoom Settings")]
    [SerializeField] private float zoomedInValue = 0.5f;
    [SerializeField] private float zoomedOutValue = 1.0f;
    [SerializeField] private float zoomSpeed = 5f;

    private CinemachineOrbitalFollow orbitalFollow;
    private float targetScale;

    private void Awake()
    {
        if (cmCamera == null)
            cmCamera = GetComponent<CinemachineCamera>();

        if (cmCamera != null)
            orbitalFollow = cmCamera.GetComponent<CinemachineOrbitalFollow>();

        targetScale = zoomedOutValue;
    }

    private void OnEnable()
    {
        if (rightClickAction == null)
            return;

        rightClickAction.action.performed += OnZoomPressed;
        rightClickAction.action.canceled += OnZoomReleased;
    }

    private void OnDisable()
    {
        if (rightClickAction == null)
            return;

        rightClickAction.action.performed -= OnZoomPressed;
        rightClickAction.action.canceled -= OnZoomReleased;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (orbitalFollow != null)
        {
            orbitalFollow.RadialAxis.Value = zoomedOutValue;
            targetScale = zoomedOutValue;
        }
    }

    private void Update()
    {
        if (orbitalFollow == null)
            return;

        float currentScale = orbitalFollow.RadialAxis.Value;

        orbitalFollow.RadialAxis.Value = Mathf.Lerp(
            currentScale,
            targetScale,
            Time.deltaTime * zoomSpeed
        );
    }

    private void OnZoomPressed(InputAction.CallbackContext context)
    {
        targetScale = zoomedInValue;
    }

    private void OnZoomReleased(InputAction.CallbackContext context)
    {
        targetScale = zoomedOutValue;
    }
}


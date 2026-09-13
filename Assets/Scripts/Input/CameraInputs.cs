using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraInputs : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cmCamera;

    [SerializeField] private InputActionReference rightClickAction;

    [SerializeField] private float zoomedInValue = 0.5f;   // Closest multiplier
    [SerializeField] private float zoomedOutValue = 2.0f;  // Default distance multiplier
    [SerializeField] private float zoomSpeed = 5f;

    private CinemachineOrbitalFollow orbitalFollow;
    private float targetScale;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (cmCamera == null) cmCamera = GetComponent<CinemachineCamera>();

        // Grab the Orbital Follow component dynamically
        orbitalFollow = cmCamera.GetComponent<CinemachineOrbitalFollow>();
        targetScale = zoomedOutValue;
    }
    void OnEnable()
    {
        rightClickAction.action.Enable();
        // Listen for when right-click is pressed or released
        rightClickAction.action.performed += OnZoomPressed;
        rightClickAction.action.canceled += OnZoomReleased;
    }

    void OnDisable()
    {
        rightClickAction.action.performed -= OnZoomPressed;
        rightClickAction.action.canceled -= OnZoomReleased;
    }

    private void OnZoomPressed(InputAction.CallbackContext context) => targetScale = zoomedInValue;
    private void OnZoomReleased(InputAction.CallbackContext context) => targetScale = zoomedOutValue;

    void Update()
    {
        if (orbitalFollow == null) return;

        // Smoothly lerp the underlying Radial Axis value
        float currentScale = orbitalFollow.RadialAxis.Value;
        orbitalFollow.RadialAxis.Value = Mathf.Lerp(currentScale, targetScale, Time.deltaTime * zoomSpeed);
    }
}

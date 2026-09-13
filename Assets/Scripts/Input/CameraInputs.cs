using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraInputs : MonoBehaviour
{
    //private Camera mainCamera;
    /*[SerializeField] private float lookSensitivity = 0.2f;
    [SerializeField] private float lookAngleLimit = 90f;
    private float lookAngle;
    */
    void Start()
    {
        //mainCamera = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    /*
    private void Update()
    {
        Vector2 mouseDelta = new Vector2(Mouse.current.delta.x.ReadValue(), Mouse.current.delta.y.ReadValue());
        Looking(mouseDelta);
    }
    */
    /*
    private void Looking(Vector2 mouseDelta)
    {
        lookAngle += -mouseDelta.y * lookSensitivity;
        lookAngle = Mathf.Clamp(lookAngle, -lookAngleLimit, lookAngleLimit);

        mainCamera.transform.localRotation = Quaternion.Euler(lookAngle, 0, 0);

    }
    */
}

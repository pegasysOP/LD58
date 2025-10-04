using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Camera playerCamera;
    public float yaw;
    public float pitch;
    public float mouseSensitivity;
    public float maxLookAngle;

    private float sensitivitySetting = 1f;
    private Mouse mouse;

    private void Awake()
    {
        sensitivitySetting = SettingsUtils.GetSensitivity();
        mouse = Mouse.current;
    }

    private void Start()
    {
        if (GameManager.Instance.cameraController)
            Debug.LogError("WARNING: Duplicate camera controller instances in scene");

        GameManager.Instance.cameraController = this;
    }

    private void Update()
    {
        if (GameManager.Instance.LOCKED)
            return;

        Vector2 mouseDelta = mouse.delta.ReadValue() * 0.02f; // Scale down to match old Input.GetAxis values

        yaw = transform.localEulerAngles.y + mouseDelta.x * mouseSensitivity * sensitivitySetting;
        pitch -= mouseDelta.y * mouseSensitivity * sensitivitySetting;
        pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);

        transform.localEulerAngles = new Vector3(0, yaw, 0);
        playerCamera.transform.localEulerAngles = new Vector3(pitch, 0, 0);
    }

    public void UpdateSensitivity(float value)
    {
        sensitivitySetting = value;
    }
}

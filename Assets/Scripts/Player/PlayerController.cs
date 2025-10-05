using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public Rigidbody rb;
    public float moveSpeed;
    public float moveAcceleration;
    public float maxVelocity;

    [Header("Jumping")]
    public GroundDetector groundDetector;
    public float jumpForce;
    public float airAcceleration;

    [Header("Debug")]
    public Vector3 inputDir;
    public float speedDebug;
    public bool groundDebug;

    private Keyboard keyboard;

    private float movementTimer = 0.5f;
    private float movementDelay = 0.5f;

    private void Start()
    {
        if (GameManager.Instance.playerController)
            Debug.LogError("WARNING: Duplicate player controller instances in scene");

        GameManager.Instance.playerController = this;
        keyboard = Keyboard.current;
    }

    private void Update()
    {
        movementTimer -= Time.deltaTime;
        if (GameManager.Instance.LOCKED)
        {
            inputDir = Vector3.zero;
            return;
        }

        if (keyboard != null && keyboard.spaceKey.wasPressedThisFrame && groundDetector.IsGrounded)
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        float horizontal = 0f;
        float vertical = 0f;

        if (keyboard != null)
        {
            if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) horizontal = -1f;
            if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) horizontal = 1f;
            if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed) vertical = 1f;
            if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed) vertical = -1f;
        }

        inputDir = new Vector3(horizontal, 0, vertical);
    }

    private void FixedUpdate()
    {
        Vector3 moveDir = transform.TransformDirection(inputDir.normalized);
        float velocityX = Mathf.Clamp(moveDir.x * moveSpeed, -maxVelocity, maxVelocity);
        float velocityZ = Mathf.Clamp(moveDir.z * moveSpeed, -maxVelocity, maxVelocity);
        Vector3 targetVelocity = new Vector3(velocityX, rb.linearVelocity.y, velocityZ);

        if (inputDir != Vector3.zero && movementTimer <= 0)
        {
            AudioManager.Instance.PlaySfxWithPitchShifting(AudioManager.Instance.walkingClips);
            movementTimer = movementDelay;
        }

        float currentAcceleration = groundDetector.IsGrounded ? moveAcceleration : airAcceleration;
        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, targetVelocity, currentAcceleration * Time.deltaTime);

        Vector3 gravity = -groundDetector.GroundNormal * Physics.gravity.magnitude * rb.mass;
        rb.AddForce(gravity, ForceMode.Acceleration);

        speedDebug = rb.linearVelocity.magnitude;
        groundDebug = groundDetector.IsGrounded;
    }
}
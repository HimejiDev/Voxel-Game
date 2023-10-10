using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    /* Variables */
    [Header("Object References")]
    [SerializeField] private Rigidbody pRigidbody;
    [SerializeField] private GameObject pCameraHolder;
    [SerializeField] private TMP_Text speedText;

    [Header("Movement Settings")]
    [SerializeField] public float speed = 5f;
    [SerializeField] private float maxSpeed = 10f;

    [Header("Jump Settings")]
    [SerializeField] public float jumpForce = 4.0f;

    [Header("Camera Settings")]
    [SerializeField] private float sensitivity;

    [Header("ReadOnly Statistics")]
    [ReadOnly][SerializeField] private float currentMoveSpeed;
    [ReadOnly][SerializeField] private bool isGrounded;

    private Vector2 moveVector, lookVector;
    private float lookRotation;

    /* Custom Functions */
    public void SetGrounded(bool state)
    {
        isGrounded = state;
    }

    private void MovePlayer()
    {
        // find target velocity
        Vector3 currentVelocity = pRigidbody.velocity;
        Vector3 targetVelocity = new(moveVector.x, 0f, moveVector.y);
        targetVelocity *= speed;
        targetVelocity = transform.TransformDirection(targetVelocity);

        // make force
        Vector3 velocityChange = targetVelocity - currentVelocity;
        velocityChange = new(velocityChange.x, 0f, velocityChange.z);
        Vector3.ClampMagnitude(velocityChange, maxSpeed);
        pRigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
    }
    private void LookPlayer()
    {
        Cursor.lockState = CursorLockMode.Locked;

        // turn player
        transform.Rotate(lookVector.x * sensitivity * Vector2.up);

        // look
        lookRotation += -lookVector.y * sensitivity;
        lookRotation = Mathf.Clamp(lookRotation, -90f, 90f);
        pCameraHolder.transform.eulerAngles = new(lookRotation, pCameraHolder.transform.eulerAngles.y, pCameraHolder.transform.eulerAngles.z);
    }
    private void JumpPlayer()
    {
        Vector3 jumpVector = Vector3.zero;

        if (isGrounded)
        {
            jumpVector = Vector3.up * jumpForce;
        }

        pRigidbody.AddForce(jumpVector, ForceMode.VelocityChange);
    }

    public void OnMovementPerformed(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }
    public void OnLookPerformed(InputAction.CallbackContext context)
    {
        lookVector = context.ReadValue<Vector2>();
    }
    public void OnJumpPerformed(InputAction.CallbackContext context)
    {
        JumpPlayer();
    }

    /* Default Functions */
    void FixedUpdate()
    {
        MovePlayer();

        // set speed text
        currentMoveSpeed = pRigidbody.velocity.magnitude;
        speedText.text = $"Speed: {Mathf.Round(currentMoveSpeed)} m/s";
    }

    void LateUpdate()
    {
        LookPlayer();
    }
}

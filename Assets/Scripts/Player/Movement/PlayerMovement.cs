using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    /* Variables */
    [Header("Object References")]
    [SerializeField] private Rigidbody pRigidbody;
    [SerializeField] private Transform pCamera;
    [SerializeField] private PlayerInput input;
    [SerializeField] private TMP_Text speedText;

    [Header("Camera Settings")]
    [SerializeField] private float sensitivity = 1f;

    [Header("Movement Settings")]
    [SerializeField] public float speed = 10f;
    [SerializeField] private float acceleration = 20f;
    [SerializeField] private float deceleration = 40f;
    [SerializeField] public float jumpForce = 1f;
    [SerializeField] public float gravity = 1f;

    [Header("ReadOnly Statistics")]
    [ReadOnly][SerializeField] private float currentMoveSpeed;

    private Vector2 moveVector = Vector2.zero;

    /* Custom Functions */
    private void OnMovementPerformed(InputAction.CallbackContext context) {
        moveVector = context.ReadValue<Vector2>();
    }

    private void OnMovementCancelled(InputAction.CallbackContext context) {
        moveVector = Vector2.zero;
    }

    /// <summary>
    /// Calculates and returns the desired velocity based on the provided movement vector and speed, considering acceleration and deceleration.
    /// </summary>
    /// <param name="movement">The movement vector.</param>
    /// <returns>The calculated velocity.</returns>
    private Vector3 CalculateVelocity(Vector3 movement) {
        // calculate the desired velocity based on moveVector and speed
        Vector3 desiredVelocity = movement * speed;

        // calculate the current velocity in the direction of desired velocity
        Vector3 currentVelocity = pRigidbody.velocity;
        currentVelocity.y = pRigidbody.velocity.y;

        if (desiredVelocity.magnitude > currentVelocity.magnitude) {
            currentVelocity += (acceleration > 0f) ? acceleration * Time.deltaTime * movement : Vector3.zero;
        } else {
            currentVelocity += (deceleration > 0f) ? deceleration * Time.deltaTime * movement : Vector3.zero;
        }

        return (acceleration > 0f || deceleration > 0f) ? currentVelocity : desiredVelocity;
    }

    /// <summary>
    /// Moves the player character based on the input moveVector, ensuring a maximum speed is maintained.
    /// </summary>
    private void MovePlayer() {
        // calculate movement vector
        Vector3 movement = new Vector3(moveVector.x, pRigidbody.velocity.y, moveVector.y);
        movement = pCamera.TransformDirection(movement);
        movement.y = pRigidbody.velocity.y;
        movement.Normalize();

        // Limit the velocity to the maximum speed (speed variable)
        Vector3 currentVelocity = Vector3.ClampMagnitude(CalculateVelocity(movement), speed);

        // Apply the final velocity to the Rigidbody
        pRigidbody.velocity = currentVelocity;
    }

    /* Default Functions */
    void Awake() {
        input = new PlayerInput();
    }

    void OnEnable() {
        input.Enable();

        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCancelled;
    }

    void OnDisable() {
        input.Disable();

        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovementCancelled;
    }

    void FixedUpdate() {
        MovePlayer();
        

        currentMoveSpeed = pRigidbody.velocity.magnitude;
        speedText.text = $"Speed: {currentMoveSpeed} m/s";
    }
}

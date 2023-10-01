using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    /* Variables */
    [Header("Object References")]
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Transform camera;
    [SerializeField] private PlayerInput input;

    [Header("Camera Settings")]
    [SerializeField] private float sensitivity = 1f;

    [Header("Movement Settings")]
    [SerializeField] public static float speed = 10f;
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
        // calculate movement
        Vector3 movement = new Vector3(moveVector.x, 0f, moveVector.y);
        movement = camera.TransformDirection(movement);
        movement.y = 0f;
        movement.Normalize();

        // Calculate the desired velocity based on moveVector and speed
        //Vector3 desiredVelocity = movement * speed;
        Vector3 currentVelocity = movement * speed;

        //// Calculate the current velocity in the direction of desired velocity
        //Vector3 currentVelocity = rb.velocity;
        //currentVelocity.y = 0f;

        //// Apply acceleration and deceleration
        //if (desiredVelocity.magnitude > currentVelocity.magnitude) {
        //    currentVelocity += movement * acceleration * Time.deltaTime;
        //} else {
        //    currentVelocity += movement * deceleration * Time.deltaTime;
        //}

        // Limit the velocity to the maximum speed (speed variable)
        currentVelocity = Vector3.ClampMagnitude(currentVelocity, speed);

        // Apply the final velocity to the Rigidbody
        rigidbody.velocity = currentVelocity;

        currentMoveSpeed = rigidbody.velocity.magnitude;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    /* Variables */
    [Header("Object References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform cam;
    [SerializeField] private PlayerInput input;

    [Header("Camera Settings")]
    [SerializeField] private float sensitivity = 1f;

    [Header("Movement Settings")]
    [SerializeField] private float speed = 1f;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private float gravity = 1f;

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
        Vector3 movement = new(moveVector.x, 0f, moveVector.y);
        movement = cam.TransformDirection(movement);
        movement.y = 0f;
        movement.Normalize();
        rb.AddForce(movement * speed);
    }
}

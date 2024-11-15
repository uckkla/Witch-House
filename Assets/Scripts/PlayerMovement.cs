using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {
    Walking,
    Climbing,
    Swimming, // Not done
}

public class PlayerMovement : MonoBehaviour
{
    private Vector3 velocity;
    private Vector3 playerMovementInput;
    private Vector2 playerMouseInput;
    private float xRot;
    private float yRot;
    private PlayerState currentState = PlayerState.Walking;

    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform playerCamera;

    [SerializeField] private float jumpForce;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float sensitivity;
    [SerializeField] private float climbSpeed;
    [SerializeField] private float gravity = -9.81f;

    // Values which only occur when drinking a potion
    private float effectSpeed;
    private float effectJumpForce;
    private Vector3 effectSize;

    private void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update(){
        playerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        playerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        switch (currentState) {
            case PlayerState.Walking:
                MovePlayer();
                MovePlayerCamera();
                break;
            case PlayerState.Climbing:
                MovePlayerCamera();
                Climb();
                break;
        }
    }

    private void MovePlayer() {
        Vector3 moveVector = transform.TransformDirection(playerMovementInput);

        if (controller.isGrounded) {
            velocity.y = -1f;

            if (Input.GetKeyDown(KeyCode.Space)) {
                velocity.y = jumpForce;
            }
        }

        else {
            velocity.y -= gravity * -2f * Time.deltaTime;
        }

        controller.Move(moveVector * playerSpeed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);
    }

    private void MovePlayerCamera() {
        xRot -= playerMouseInput.y * sensitivity;
        xRot = Mathf.Clamp(xRot, -90f, 90f);
        transform.Rotate(0f, playerMouseInput.x * sensitivity, 0f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
    }
    private void Climb() {
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 climbMovement = new Vector3(0, verticalInput * climbSpeed, 0);
        controller.Move(climbMovement * Time.deltaTime);

        // Player trying to get off ladder from anywhere other than the top
        if (Input.GetKeyDown(KeyCode.Space)) {
            Vector3 pushDirection = -transform.forward;
            Vector3 pushForce = pushDirection * jumpForce;

            controller.Move(pushForce * Time.deltaTime);

            currentState = PlayerState.Walking;
        }
    }

    public void ApplyEffect(Recipe recipe) {
        Debug.Log("Applying effect...");
        playerSpeed *= recipe.getSpeedMultiplier;
        jumpForce *= recipe.getJumpForceMultiplier;
        transform.localScale *= recipe.getSizeMultiplier;

        StartCoroutine(RevertEffects(recipe));
    }

    private IEnumerator RevertEffects(Recipe recipe) {
        yield return new WaitForSeconds(recipe.getEffectDuration);

        playerSpeed /= recipe.getSpeedMultiplier;
        jumpForce /= recipe.getJumpForceMultiplier;
        transform.localScale /= recipe.getSizeMultiplier;

        Debug.Log("Potion effect ended");

    }

    public void setCurrentState(PlayerState currentState) {
        this.currentState = currentState;
    }
}

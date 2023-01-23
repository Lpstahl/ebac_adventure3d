using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player1 : MonoBehaviour
{
    public Animator animator;

    public CharacterController characterController;
    public float speed = 1f;
    public float turnSpeed = 1f;
    public float gravity = 9.8f;

    private float vSpeed = 0f;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    PlayerInput playerInput;
    bool isMovementPressed;
    private void Awake()
    {
        playerInput = new PlayerInput();

        playerInput.CharacterController.Move.started += onMovementInput;
        playerInput.CharacterController.Move.canceled += onMovementInput;
        playerInput.CharacterController.Move.performed += onMovementInput;
    }

    void onMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    private void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        var InputAxisVertical = Input.GetAxis("Vertical");
        var speedvector = transform.forward * InputAxisVertical * speed;

        vSpeed -= gravity * Time.deltaTime;
        speedvector.y = vSpeed;

        characterController.Move(speedvector * Time.deltaTime);

        //esse código serve para if/else pq sempre vai ser 2 condições(true or false)
        animator.SetBool("Run", InputAxisVertical != 0);

    }
}
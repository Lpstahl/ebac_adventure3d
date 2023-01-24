using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    public CharacterController characterController;
    public float speed = 1f;
    public float turnSpeed = 1f;
    public float gravity = 9.8f;
    public float jumpSpeed = 15f;

    [Header("Run Setup")]
    public KeyCode keyrun = KeyCode.LeftShift;
    public float speedRun = 1.5f;

    private int jumpCount = 0;
    private int maxJumpCount = 2;
    private float vSpeed = 0f;

    private void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        var InputAxisVertical = Input.GetAxis("Vertical");
        var speedvector = transform.forward * InputAxisVertical * speed;

        //running
        var isWalking = InputAxisVertical != 0;
        if (isWalking)
        {
            if (Input.GetKey(keyrun))
            {
                speedvector *= speedRun;
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }
        }

        vSpeed -= gravity * Time.deltaTime;
        speedvector.y = vSpeed;

        characterController.Move(speedvector * Time.deltaTime);

        //ANIMATOR MOVEMENT
        animator.SetBool("isWalking", InputAxisVertical != 0);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            vSpeed = jumpSpeed;
            animator.SetBool("isJumping", true);
            jumpCount++;
        }
        else
        {
            animator.SetBool("isJumping", false);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.normal.y > 0.1f)
        {
            jumpCount = 0;
        }
    }
}

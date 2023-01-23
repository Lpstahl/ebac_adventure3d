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

    private float vSpeed = 0f;

    private void Update()
    {
        Move();
        Jump();
        isGorounded();
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
                animator.SetBool("isRunning", InputAxisVertical != 0);
            }
        }

        vSpeed -= gravity * Time.deltaTime;
        speedvector.y = vSpeed;

        characterController.Move(speedvector * Time.deltaTime);

        //ANIMATOR MOVEMENT
        //esse código serve para if/else pq sempre vai ser 2 condições(true or false)
        animator.SetBool("isWalking", InputAxisVertical != 0);
    }

    private void Jump()
    {
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGorounded())
            {
                vSpeed = jumpSpeed;
                animator.speed = speedRun;
                animator.SetBool("isJumping", true);
            }
            else 
            {
                animator.speed = 1;
                animator.SetBool("isJumping", false);
            }
        }
    }

    private bool isGorounded()
    {
       return characterController.isGrounded;
    }
}
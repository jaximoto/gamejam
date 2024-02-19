using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Public variables changed in editor
    public float moveSpeed;
    public float rotationSpeed;

    // Component variables
    private Animator animator;
    private Rigidbody2D rb;

    // Global movement variables
    private float moveY;
    private float moveX;
    private Vector2 moveDirection;
    private Quaternion rotation;

    // Animation variables
    int isMovingHash;
    int isStabbingHash;

    /************************************************
     *-------------CORE UNITY FUNCTIONS-------------*
     ************************************************/
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isMovingHash = Animator.StringToHash("isMoving");
        isStabbingHash = Animator.StringToHash("isStabbing");
    }

    void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        // Physics Calculations
        Move();
        RotateInDirectionOfInput();
    }

    /*********************************************
     *------------Input Proccessor------------*
     *********************************************/
    void ProcessInputs()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        if (moveDirection != Vector2.zero)
        {
            animator.SetBool(isMovingHash, true);
        }
        else
        {
            animator.SetBool(isMovingHash, false);
        }

        if (Input.GetAxisRaw("Fire1") != 0)
        {
            animator.SetBool(isStabbingHash, true);
            Debug.Log("stabbed");
        }
        else
        {
            animator.SetBool(isStabbingHash, false);
        }
    }

    /*********************************************
     *---------Movement Helper Functions---------*
     *********************************************/
    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed,
                                  moveDirection.y * moveSpeed);


    }

    void RotateInDirectionOfInput()
    {
        if (moveDirection != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward,
                                                          moveDirection);
            rotation = Quaternion.RotateTowards(transform.rotation,
                                                           targetRotation,
                                               rotationSpeed * Time.deltaTime);
            rb.MoveRotation(rotation);
        }
    }
}

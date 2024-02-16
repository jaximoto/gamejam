using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;

    private Rigidbody2D rb;
    private float moveY;
    private float moveX;
    private Vector2 moveDirection;
    private Quaternion rotation;

    /************************************************
     *-------------CORE UNITY FUNCTIONS-------------*
     ************************************************/
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
     *---------Movement Helper Functions---------*
     *********************************************/
    void ProcessInputs()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        
        moveDirection = new Vector2(moveX, moveY).normalized;

    }

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

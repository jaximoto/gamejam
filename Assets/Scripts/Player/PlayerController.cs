using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, IDamageable
{
    // Public variables changed in editor
    public float moveSpeed;
    public float rotationSpeed;
    public float stabDelay = 0.5f;
    public float sneakSpeed;


    // Component variables
    private Animator animator;
    private Rigidbody2D rb;

    // Global movement variables
    private float speed;
    private float moveY;
    private float moveX;
    private Vector2 moveDirection;
    private Quaternion rotation;

    // Animation variables
    int isMovingHash;
    int isStabbingHash;

    // Cooldown variables
    private float nextStab = 0.15f;

    // Health bar variables
    private int health = 3;

    private Image healthBarImage;
    public Sprite twoThirdsHealth;
    public Sprite oneThirdsHealth;
    public Sprite noHealth;
    public Sprite fullHealth;
    /************************************************
     *-------------CORE UNITY FUNCTIONS-------------*
     ************************************************/
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isMovingHash = Animator.StringToHash("isMoving");
        isStabbingHash = Animator.StringToHash("isStabbing");
        GameObject healthBar = GameObject.Find("HealthBar");
        healthBarImage = healthBar.GetComponent<Image>();




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

        if (Input.GetAxisRaw("Fire1") != 0 && Time.time > nextStab)
        {
            animator.SetBool(isStabbingHash, true);
            // TODO add stabbing code to kill
            nextStab = Time.time + stabDelay;
            //Debug.Log(animator.GetBool(isStabbingHash));
        }
        else
        {
            animator.SetBool(isStabbingHash, false);
        }

        if (Input.GetButton("Fire3") == true) 
        {
            speed = sneakSpeed;
        }
        else 
        {
            speed = moveSpeed;
        }
    }

    /*********************************************
     *---------Movement Helper Functions---------*
     *********************************************/
    void Move()
    { 

        rb.velocity = new Vector2(moveDirection.x * speed,
                                  moveDirection.y * speed);


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

    /*********************************************
     *------------Interface Functions------------*
     *********************************************/
    public void Damage()
    {
        Debug.Log("Took Damage");
        health--;
        
        switch(health)
        {
            case 2:
                healthBarImage.sprite = twoThirdsHealth;
                break;
            case 1:
                healthBarImage.sprite = oneThirdsHealth;
                break;
            case 0:
                healthBarImage.sprite = noHealth;
                Debug.Log("dead");
                // die
                break;
            default: break;
        }
    }
}

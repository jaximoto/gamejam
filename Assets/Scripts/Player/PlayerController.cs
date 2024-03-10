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

    // Disguise variable
    private string disguise = "0";

    // Global movement variables
    private float speed;
    private float moveY;
    private float moveX;
    private Vector2 moveDirection;
    private Quaternion rotation;

    // Animation variables
    
    private string currentAnimationState;
    const string PLAYER_IDLE = "Idle";
    const string PLAYER_STAB = "isStabbing";
    const string PLAYER_MOVE = "isMoving";
    private float nextAnim = 0f;
    private float animDelay = 0f;


    // Stab variables
    private bool isStabbing = false;
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
        GameObject healthBar = GameObject.Find("HealthBar");
        healthBarImage = healthBar.GetComponent<Image>();




    }

    void Update()
    {
        
        ProcessInputs();
        //animator.Play("isStabbing1");
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

        /*
        if (moveDirection != Vector2.zero)
        {
            ChangeAnimationState(disguise, PLAYER_MOVE);
            //animator.SetBool(isMovingHash, true);
        }
        else
        {
           // ChangeAnimationState(disguise, PLAYER_IDLE);
            //animator.SetBool(isMovingHash, false);
        }
        */
        if (Input.GetAxisRaw("Fire1") != 0 && Time.time > nextStab)
        {
            
            ChangeAnimationState(disguise, PLAYER_STAB, 0.5f);
            //Debug.Log(currentAnimationState);
            //animator.SetBool(isStabbingHash, true);
            // TODO add stabbing code to kill
            
            nextStab = Time.time + stabDelay;
            isStabbing = true;
            //Debug.Log(animator.GetBool(isStabbingHash));
        }
        else
        {
            if (moveDirection != Vector2.zero)
            {
                ChangeAnimationState(disguise, PLAYER_MOVE);
            }
            else
            {
                ChangeAnimationState(disguise, PLAYER_IDLE);
            }
            isStabbing = false;
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
     *---------Physics Helper Functions----------*
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

    void Stab()
    {
        
    }


    /*********************************************
     *------------Animation Functions------------*
     *********************************************/
    public void ChangeAnimationState(string disguise, string animation, float delay = 0)
    {
        // Set delay until next animation can play if 0 no delay for next animation
        animDelay = delay;

        // Build full animation string
        string tmp = animation + disguise;

        // Stop same animation from interrupting self
        if (currentAnimationState == tmp)
            return;

        if (Time.time > nextAnim)
        {
            //Debug.Log(tmp);
            animator.Play(tmp);
            currentAnimationState = tmp;
            nextAnim = Time.time + animDelay;
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

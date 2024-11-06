using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenMovement : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private float speed = 4f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float rollForce = 6f;
    [SerializeField] private bool noBlood = false;
    [SerializeField] private bool canMove = true;
    //[SerializeField] private GameObject slideDust;

    private Animator animator;
    private Rigidbody2D rb;
    private Sensor_Ren groundSensor;
    private Sensor_Ren wallSensorR1, wallSensorR2, wallSensorL1, wallSensorL2;
    private SpriteRenderer spriteRenderer;

    private bool isWallSliding = false;
    private bool grounded = false;
    private bool rolling = false;
    private int facingDirection = 1;
    private int currentAttack = 0;
    private float timeSinceAttack = 0.0f;
    private float delayToIdle = 0.05f;
    private readonly float rollDuration = 8.0f / 14.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        groundSensor = transform.Find("GroundSensor")?.GetComponent<Sensor_Ren>();
        wallSensorR1 = transform.Find("WallSensor_R1")?.GetComponent<Sensor_Ren>();
        wallSensorR2 = transform.Find("WallSensor_R2")?.GetComponent<Sensor_Ren>(); 
        wallSensorL1 = transform.Find("WallSensor_L1")?.GetComponent<Sensor_Ren>();
        wallSensorL2 = transform.Find("WallSensor_L2")?.GetComponent <Sensor_Ren>();

        if (!groundSensor)
        {
            Debug.LogWarning("Sensor Missing1");
        }
        if ( !wallSensorR1 )
        {
            Debug.LogWarning("Sensor Missing2");
        }
        if ( !wallSensorR2 )
        {
            Debug.LogWarning("Sensor Missing3");
        }
        if (!wallSensorL1)
        {
            Debug.LogWarning("Sensor Missing4");
        }
        if ( !wallSensorL2)
        {
            Debug.LogWarning("Sensor Missing5");
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceAttack += Time.deltaTime;
        //Update grounded status
        UpdateGroundedStatus();
        //handle input and Movement
        HandleInput();
        HandleAnimations();
    }

    private void UpdateGroundedStatus()
    {
        bool wasGrounded = grounded;
        grounded = groundSensor != null && groundSensor.State();
        //Debug.Log("Player on Ground");

        if(grounded != wasGrounded )
        {
            animator.SetBool("Grounded", grounded);
        }
    }

    private void HandleInput()
    {

        float inputX = Input.GetAxis("Horizontal");

        // Handle facing direction
        if(inputX != 0 && canMove)
        {
            facingDirection = (int)Mathf.Sign(inputX);
            spriteRenderer.flipX = facingDirection == -1;
        }

        //Set velocity
        if (!rolling && canMove)
        {
            rb.velocity = new Vector2(inputX * speed, rb.velocity.y);
        }

        //Handle other actions
        if(Input.GetKeyDown(KeyCode.Space) && grounded && !rolling)
        {
            Jump();
        }

        if(Input.GetMouseButtonDown(0) && timeSinceAttack > 0.25f && !rolling)
        {
            Attack();
        }
        if(Input.GetMouseButtonDown(1) && !rolling)
        {
            
            canMove = false;
            Block();
            
        }
        if (Input.GetMouseButtonUp(1))
        {
            animator.SetBool("IdleBlock", false);
            canMove = true;
        }
        if(Input.GetKeyDown(KeyCode.LeftShift) && !rolling && !isWallSliding)
        {
            Roll();
        }

        //set airspeedy in animator
        animator.SetFloat("AirSpeedY", rb.velocity.y);
    }

    private void Jump()
    {
        animator.SetTrigger("Jump");
        grounded = false;
        animator.SetBool("Grounded", grounded);
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        groundSensor?.Disable(0.2f);
    }

    private void Attack()
    {
        currentAttack = (timeSinceAttack > 1) ? 1 : currentAttack + 1;
        if(currentAttack > 3)
        {
            currentAttack = 1;
        }

        animator.SetTrigger("Attack" + currentAttack);
        timeSinceAttack = 0;
    }

    private void Block()
    {
        animator.SetTrigger("Block");
        animator.SetBool("IdleBlock", true);
    }

    private void Roll()
    {
        rolling = true;
        animator.SetTrigger("Roll");
        rb.velocity = new Vector2(facingDirection * rollForce, rb.velocity.y);
        Invoke(nameof(EndRoll), rollDuration);
    }

    private void EndRoll()
    {
        rolling = false;
    }

    private void HandleAnimations()
    {

        // Wall Slide
        /*sWallSliding = IsWallSliding();
        animator.SetBool("wallSlide", isWallSliding);*/

        if(Mathf.Abs(rb.velocity.x)> Mathf.Epsilon)
        {
            delayToIdle = 0.05f;
            animator.SetInteger("AnimState", 1);
        }
        else if(delayToIdle > 0)
        {
            delayToIdle -= Time.deltaTime;
            if(delayToIdle <= 0)
            {
                animator.SetInteger("AnimState", 0);
            }
        }
        
    }

    private bool IsWallSliding()
    {
        return (wallSensorL1.State() && wallSensorL2.State() || 
            wallSensorR1.State() && wallSensorR2.State());
    }
}

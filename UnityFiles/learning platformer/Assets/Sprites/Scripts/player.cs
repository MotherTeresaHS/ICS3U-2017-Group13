using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void DeadEventHandler();

public class player : Character
{

    private static player instance;

    // Is an Event when trigger will show the player is dead to the AI
    public event DeadEventHandler Dead;

    public static player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<player>();
            }
            
            return instance;
        }

        
    }      

    [SerializeField]
    private Transform[] groundPoints;


    [SerializeField]
    private bool airControl;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private float standingJumpForce;

    [SerializeField]
    private Stats healthStat;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private float playerHealth;

    private bool immortal = false;

    
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float immortalTime;

    [SerializeField]
    private LayerMask whatIsGround;
       
    public Rigidbody2D MyRigidbody { get; set; }

    private float direction;

    private bool move;

    private float btnHorizontal;

    public bool Slide { get; set; }

    public bool Jump { get; set; }

    public bool OnGround { get; set; }



    public override bool IsDead
    {
        get
        {
            if (HealthStat.CurrentValue <= 0)
            {
                OnDead();
            }
            
            return HealthStat.CurrentValue <= 0;
        }
    }

    public Stats HealthStat
    {
        get
        {
            return healthStat;
        }

        set
        {
            healthStat = value;
        }
    }

    public float PlayerHealth
    {
        get
        {
            return playerHealth;
        }

        set
        {
            playerHealth = value;
        }
    }

    private Vector2 startPos;



    // Use this for initialization
    private void Awake()
    {
        HealthStat.Initialize();
    }
    public override void Start()
    {
        base.Start();
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        MyRigidbody = GetComponent<Rigidbody2D>();

        startPos = new Vector2(-17, -3);
        
    }

    void Update()
    {
        if (!TakingDamage && !IsDead)
        {
            if (transform.position.y <= -10f)
            {
                Death();
            }
            HandleInput();
        }
        
    }

    // Update is called once per frame
    // Fixed at 50 FPS
    void FixedUpdate()
    {
        if (!TakingDamage && !IsDead)
        {
            float horizontal = Input.GetAxis("Horizontal");
            HandleMovement(horizontal);

            OnGround = IsGrounded();

            if (move)
            {
                this.btnHorizontal = Mathf.Lerp(btnHorizontal, direction, Time.deltaTime * 2);
                HandleMovement(btnHorizontal);
                Flip(direction);

            }
            else
            {
                HandleInput();

                Flip(horizontal);
            }

            

            HandleLayers();
        }
        

    }

    public void OnDead()
    {
        if (Dead != null)
        {
            Dead();
        }
    }


    private void HandleMovement(float horizontal)
    {
        if(MyRigidbody.velocity.y < 0)
        {
            MyAnimator.SetBool("land", true);
        }
        if(!Attack && !Slide && (OnGround || airControl))
        {
            MyRigidbody.velocity = new Vector2(horizontal * movementSpeed, MyRigidbody.velocity.y);
        }
        //if (Jump && MyRigidbody.velocity.y == 0)
        //{
        //    MyRigidbody.AddForce(new Vector2(0, jumpForce));
        //}


        MyAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }
    // makes sprite attack


    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MyAnimator.SetTrigger("jump");
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            MyAnimator.SetTrigger("attack");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            MyAnimator.SetTrigger("slide");
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            MyAnimator.SetTrigger("throw");            
        }
    }


    private void Flip(float horizontal)
    {
        // Saying if the player is facing right or left
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            ChangeDirection();
        }
    }

    private bool IsGrounded()
    {
        if (MyRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {

                        return true;
                    }
                }

            }
        }
        return false;
    }
    private void HandleLayers()
    {
        if (!OnGround)
        {
            // sets the layer to one
            MyAnimator.SetLayerWeight(1, 1);

        }
        else
        {
            
            MyAnimator.SetLayerWeight(1, 0);
        }
    }
    public override void ThrowKnife(int value)
    {
        if(!OnGround && value == 1 || OnGround && value == 0)
        {
            base.ThrowKnife(value);
        }
    }

    private IEnumerator IndicateImmortal()
    {
        while (immortal)
        {
            // will run as long as we are immortal
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.1f);
            
        }
    }

    public override IEnumerator TakeDamage()
    {
        
        

        if (!immortal)
        {
            
            HealthStat.CurrentValue -= 10;
            
            GameManager.Instance.CollectedCoins--;
           

            if (!IsDead)
            {
                if (GameManager.Instance.CollectedCoins <= 0)
                {

                    GameManager.Instance.CollectedCoins = 0;
                }
                MyAnimator.SetTrigger("damage");
                immortal = true;
                StartCoroutine(IndicateImmortal());
                yield return new WaitForSeconds(immortalTime);
                
                immortal = false;

            }
            else
            {
                
                MyAnimator.SetLayerWeight(1, 0);
                MyAnimator.SetTrigger("die");
                
            }
        }
        
    }

    public override void Death()
    {
        // resets player on death
        
        MyRigidbody.velocity = Vector2.zero;
        MyAnimator.SetTrigger("idle");
        HealthStat.CurrentValue = HealthStat.MaxValue;
        //transform.position = startPos;
        SceneManager.LoadScene("MainMenu");


    }

    public void BtnJump()
    {
        // Used for button to make player jump

        if (OnGround && !IsDead)
        {
            MyAnimator.SetTrigger("jump");
            MyRigidbody.AddForce(new Vector2(0, jumpForce));


            Jump = true;
        }

    }
    public void BtnAttack()
    {
        // Used for button to make player attack
        MyAnimator.SetTrigger("attack");

    }

    public void BtnSlide()
    {
        // Used for button to make player slide
        MyAnimator.SetTrigger("slide");

    }
    public void BtnThrow()
    {
        // Used for button to make player Throw
        MyAnimator.SetTrigger("throw");

    }
    public void BtnMove(float direction)
    {
        // Used for button to make player move
        this.direction = direction;
        this.move = true;

    }
    public void BtnStopMove()
    {
        this.direction = 0;
        this.btnHorizontal = 0;
        move = false;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Coin")
        {
            GameManager.Instance.CollectedCoins++;
            Destroy(other.gameObject);
        }
    }
    
}

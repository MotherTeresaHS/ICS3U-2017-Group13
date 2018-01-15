﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

    private IEnemyState currentState;

    public GameObject Target { get; set; }

    [SerializeField]
    private float meleeRange;
    [SerializeField]
    private float throwRange;
    [SerializeField]
    private Transform leftEdge;
    [SerializeField]
    private Transform rightEdge;

    private bool dropItem = true; 

    public bool InMeleeRange
    {
        get
        {
            if (Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= meleeRange;
            }
            else
            {
                return false;
            }
        }
    }
    public bool InThrowRange
    {
        get
        {
            if (Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= throwRange;
            }
            else
            {
                return false;
            }
        }
    }

    public override bool IsDead
    {
        get
        {
            return health <= 0;
        }
    }



    private Vector2 startPos;


    // Use this for initialization
    public override void Start ()
    {
        base.Start();
        GetComponent<Rigidbody2D>().isKinematic = true;
        player.Instance.Dead += new DeadEventHandler(RemoveTarget);

        ChangeState(new IdleState());
        startPos = transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!IsDead)
        {
            if (!TakingDamage)
            {
                currentState.Execute();
            }          
            LookAtTarget();
        }

        
	}

    public void RemoveTarget()
    {
        Target = null;
        ChangeState(new PatrolState());
    }
    

    private void LookAtTarget()
    {
        if(Target != null)
        {
            // checks to see if target is on the left or right
            float xDir = Target.transform.position.x - transform.position.x;

            if (xDir <0 && facingRight || xDir > 0 && !facingRight)
            {
                //changes direction
                ChangeDirection();
            }
        }
    }

    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
            
        }
        currentState = newState;
        currentState.Enter(this);
    }

    public void Move()
    {
        //If the enemy isn't attacking
        if (!Attack)
        {
            if ((GetDirection().x > 0 && transform.position.x < rightEdge.position.x) || (GetDirection().x < 0 && transform.position.x > leftEdge.position.x))
            {
                //Sets the speed to 1 to move the enemy
                MyAnimator.SetFloat("speed", 1);

                //Moves the Enemy the right way
                transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
            }
            else if (currentState is PatrolState)
            {
                ChangeDirection();
            }
            else if (currentState is RangedState)
            {
                Target = null;
                ChangeState(new IdleState());
            }
            
        }
    }
    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }
     public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        currentState.OnTriggerEnter(other);
        
    }

    public override IEnumerator TakeDamage()
    {
        
        health -= 10;

        if (!IsDead)
        {
            MyAnimator.SetTrigger("damage");
        }
        else
        {
            if (dropItem)
            {
                GameObject coin = (GameObject)Instantiate(GameManager.Instance.CoinPreFab, new Vector3(transform.position.x, transform.position.y + 2), Quaternion.identity);
                Physics2D.IgnoreCollision(coin.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                dropItem = false;

            }

            MyAnimator.SetTrigger("die");
        }

        yield return null;
    }

    public override void Death()
    {
        dropItem = true;
        MyAnimator.ResetTrigger("die");
        MyAnimator.SetTrigger("idle");
        health = 20;
        transform.position = startPos;
    }
}

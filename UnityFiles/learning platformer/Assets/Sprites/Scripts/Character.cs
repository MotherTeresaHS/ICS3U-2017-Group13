// Created by Andre Hazim
// From Nov - Jan 2017-2018


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

    

    

    [SerializeField]
    protected Transform knifepos;

    [SerializeField]
    protected float movementSpeed;

    protected bool facingRight;

    [SerializeField]
    private GameObject knifePrefab;

    [SerializeField]
    protected float health;

    [SerializeField]
    private EdgeCollider2D swordCollider;

    [SerializeField]
    private List<string> damageSources;

    public abstract bool IsDead { get; }

    public bool Attack { get; set; }

    public bool TakingDamage { get; set; }

    public Animator MyAnimator { get; private set; }

    public EdgeCollider2D SwordCollider
    {
        get
        {
            return swordCollider;
        }

        
    }


    // Use this for initialization
    public virtual void Start ()
    {
        facingRight = true;
        MyAnimator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeDirection()
    {
        // makes the player flips sides
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }
    public virtual void ThrowKnife(int value)
    {
        if (facingRight)
        {
            // throws the knife right
            GameObject tmp = Instantiate(knifePrefab, knifepos.position, Quaternion.Euler(new Vector3(0, 0, -90)));
            tmp.GetComponent<Knife>().Initiazlize(Vector2.right);
        }
        else
        {
            // throws the knife left
            GameObject tmp = Instantiate(knifePrefab, knifepos.position, Quaternion.Euler(new Vector3(0, 0, 90)));
            tmp.GetComponent<Knife>().Initiazlize(Vector2.left);
        }
    }

    public abstract IEnumerator TakeDamage();

    public abstract void Death();

    public void MeleeAttack()
    {
        SwordCollider.enabled = true;

    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (damageSources.Contains(other.tag))
        {
            // sword damage
            if (other.tag == "Sword")
            {
                health -= 20;
            }
            StartCoroutine(TakeDamage());
        }
        
    }
}

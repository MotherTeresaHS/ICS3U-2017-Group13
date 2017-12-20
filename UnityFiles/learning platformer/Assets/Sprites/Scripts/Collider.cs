﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider : MonoBehaviour {

    

    [SerializeField]
    private BoxCollider2D platformCollider;

    [SerializeField]
    private BoxCollider2D platformTrigger;

	// Use this for initialization
	void Start () {

        
        Physics2D.IgnoreCollision(platformCollider, platformTrigger, true);
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D other)
    {
	    if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(platformCollider, other, true);
        }	
	}
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(platformCollider, other, false);
        }
    }
}
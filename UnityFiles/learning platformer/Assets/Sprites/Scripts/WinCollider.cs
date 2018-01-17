using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCollider : MonoBehaviour {

    [SerializeField]
    private BoxCollider2D Knife;

    [SerializeField]
    private BoxCollider2D EnemyKnife;

    // Use this for initialization
    void Start()
    {


        Physics2D.IgnoreCollision(Knife, EnemyKnife, true);
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EndLevel" || other.gameObject.tag == "EnemyKnife")
        {
            Physics2D.IgnoreCollision(Knife, other, true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
     
    }
}




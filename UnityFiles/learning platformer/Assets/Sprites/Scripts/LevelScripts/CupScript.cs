using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupScript : MonoBehaviour {
    [SerializeField]
    private GameObject Knife;
    [SerializeField]
    private GameObject EnemyKnife;
    [SerializeField]
    private GameObject EndLevel;
    void OnTriggerEnter2D(Collider2D col)
	{
        
        
        Physics2D.IgnoreCollision(Knife.GetComponent<Collider2D>(), EndLevel.GetComponent<Collider2D>(), true);
        Physics2D.IgnoreCollision(EnemyKnife.GetComponent<Collider2D>(), EndLevel.GetComponent<Collider2D>(), true);

        LevelControlScript.Instance.youWin ();
	}
}

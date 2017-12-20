using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishedLevel : MonoBehaviour {

    [SerializeField]
    private Transform pannel;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            pannel.gameObject.SetActive(true);
            Time.timeScale = 0;

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Time.timeScale = 1;
    }
}


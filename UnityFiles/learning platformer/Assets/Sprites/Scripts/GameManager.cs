using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private static GameManager instance;

    [SerializeField]
    private GameObject coinPreFab;

    [SerializeField]
    private Text coinText;
    
    

    private int collectedCoins;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }

        
    }

    public GameObject CoinPreFab
    {
        get
        {
            return coinPreFab;
        }

        
    }

    public int CollectedCoins
    {
        get
        {
            return collectedCoins;
        }

        set
        {
            coinText.text = value.ToString();
            collectedCoins = value;
        }
    }
    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

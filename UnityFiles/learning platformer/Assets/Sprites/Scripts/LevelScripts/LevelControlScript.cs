using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelControlScript : MonoBehaviour {



    private static LevelControlScript instance = null;
    GameObject youWinText;
	int sceneIndex, levelPassed;

    public int SceneIndex
    {
        get
        {
            return sceneIndex;
        }

        set
        {
            sceneIndex = value;
        }
    }

    public static LevelControlScript Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    // Use this for initialization
    void Start () {
		
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy (gameObject);

		
		
		youWinText = GameObject.Find("YouWinText");
        youWinText.gameObject.SetActive(false); 
     
		

		SceneIndex = SceneManager.GetActiveScene ().buildIndex;
		levelPassed = PlayerPrefs.GetInt ("LevelPassed");
	}

	public void youWin()
	{
		if (SceneIndex == 4) { 
            youWinText.gameObject.SetActive (true);
			Invoke ("loadMainMenu", 1f);
            }
		else {
			if (levelPassed < SceneIndex)
				PlayerPrefs.SetInt ("LevelPassed", SceneIndex);
			
			    youWinText.gameObject.SetActive (true);
                
			    Invoke ("loadNextLevel", 1f);
		}
	}

	public void youLose()
	{
		
		
		Invoke ("loadMainMenu", 1f);
	}

	void loadNextLevel()
	{
		SceneManager.LoadScene ("LevelSelect");
        
	}

	void loadMainMenu()
	{
		SceneManager.LoadScene ("LevelSelect");
	}
}

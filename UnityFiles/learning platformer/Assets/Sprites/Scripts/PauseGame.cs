// Created by Andre Hazim
// From Nov - Jan 2017-2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour {
    [SerializeField]
    private Transform canvas;


    // Update is called once per frame
    public void BtnPause()
    // Button for Pausing the game
    {
        if (canvas.gameObject.activeInHierarchy ==  false)
        {
            canvas.gameObject.SetActive(true);
            // stops everything to make it looked paused
            Time.timeScale = 0;
        }
        
       
    }
    public void BtnResume()
    // Button for Resuming the Game
    {
        canvas.gameObject.SetActive(false);
        //Resumes everything
        Time.timeScale = 1;
    }
    public void BtnSettings(string sceneName)
    // Button for going to the settings
    {
        canvas.gameObject.SetActive(false);
        SceneManager.LoadScene(sceneName);
        //Resumes everything
        Time.timeScale = 1;
    }
    public void BtnMainMenu(string sceneName)
    // Button for going to the main menu
    {
        canvas.gameObject.SetActive(false);
        SceneManager.LoadScene(sceneName);
        //Resumes everything
        Time.timeScale = 1;
        transform.position = new Vector3(17, 3, 0);
    }
}

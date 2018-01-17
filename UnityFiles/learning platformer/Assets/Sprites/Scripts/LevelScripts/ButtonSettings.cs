// Created by Andre Hazim
// From Nov - Jan 2017-2018
//https://www.youtube.com/watch?v=jk5zKNhXCmc

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonSettings : MonoBehaviour
{
    private static ButtonSettings instance;
    public static int releasedLevelStatic = 1;
    
    public int releasedLevel;
    public string nextLevel;

    public int ReleasedLevel
    {
        get
        {
            return releasedLevel;
        }

        set
        {
            releasedLevel = value;
        }
    }

    public static ButtonSettings Instance
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

    void Awake()
    {
       
    }

    public void ButtonNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
        if (releasedLevelStatic <= ReleasedLevel)
        {
            releasedLevelStatic = ReleasedLevel;
            PlayerPrefs.SetInt("Level", releasedLevelStatic);
            Time.timeScale = 1;

        }
    }
    public void ButtonMenu()
    {
        SceneManager.LoadScene("MenuLevel");
    }
}
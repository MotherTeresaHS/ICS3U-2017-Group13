// Created by Andre Hazim
// From Nov - Jan 2017-2018
//https://www.youtube.com/watch?v=jk5zKNhXCmc

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonSettings : MonoBehaviour
{
    public static int releasedLevelStatic = 1;
    public int releasedLevel;
    public string nextLevel;


    void Awake()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            releasedLevelStatic = PlayerPrefs.GetInt("Level", releasedLevelStatic);

        }
    }

    public void ButtonNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
        if (releasedLevelStatic <= releasedLevel)
        {
            releasedLevelStatic = releasedLevel;
            PlayerPrefs.SetInt("Level", releasedLevelStatic);
            Time.timeScale = 1;

        }
    }
    public void ButtonMenu()
    {
        SceneManager.LoadScene("MenuLevel");
    }
}
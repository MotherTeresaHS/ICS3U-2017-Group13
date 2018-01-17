using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGame : MonoBehaviour {
    [SerializeField]
    private Transform Player;
    

    void Awake()
    {
        
        GameManager.Instance.CollectedCoins = PlayerPrefs.GetInt("Score", 0);
        player.Instance.HealthStat.CurrentValue = PlayerPrefs.GetFloat("Health", 100);


       



        Player.position = new Vector3(PlayerPrefs.GetFloat("playerX"), PlayerPrefs.GetFloat("playerY"), PlayerPrefs.GetFloat("playerZ"));
        Player.eulerAngles = new Vector3(0, PlayerPrefs.GetFloat("CamY"), 0);

        

    }

    public void Update()
    {
       
    }
    public void SaveGameSettings(bool Quit)
    {

    
        PlayerPrefs.SetFloat("Health", player.Instance.HealthStat.CurrentValue);
        
        PlayerPrefs.SetInt("Score", GameManager.Instance.CollectedCoins);

       

        PlayerPrefs.SetFloat("playerX", Player.position.x);
        PlayerPrefs.SetFloat("playerY", Player.position.y);
        PlayerPrefs.SetFloat("playerZ", Player.position.z);
        PlayerPrefs.SetFloat("CamY", Player.eulerAngles.y);
        if (Quit)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
        }
       
    }
    public void LoadGame()
    {

        
    }
    
    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// creates background music for the game
public class backgroundsound : MonoBehaviour
{
    private static backgroundsound instance = null;
    private static backgroundsound Instance
    {
        get { return instance; }
    }
// allows it so that the music can continue through each scene
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
        }
    }

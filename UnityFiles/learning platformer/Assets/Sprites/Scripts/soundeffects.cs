using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundeffects : MonoBehaviour
{

    public static AudioClip throwknifeSound, jumpSound, slideSound, attackSound, collectcoinSound, pressbuttonSound, gethitSound, dieSound;
    static AudioSource audioSrc;

    // Use this for initialization
    void Start()
    {
        throwknifeSound = Resources.Load<AudioClip>("throwknife");
        jumpSound = Resources.Load<AudioClip>("whoosh");
        slideSound = Resources.Load<AudioClip>("slide");
        attackSound = Resources.Load<AudioClip>("sword");
        collectcoinSound = Resources.Load<AudioClip>("coincollect");
        pressbuttonSound = Resources.Load<AudioClip>("boop");
        gethitSound = Resources.Load<AudioClip>("hurtsound");
        dieSound = Resources.Load<AudioClip>("deathsound");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void playSound(string clip)
    {
        switch (clip)
        {
            case "throwknife":
                audioSrc.PlayOneShot(throwknifeSound);
                break;

            case "whoosh":
                audioSrc.PlayOneShot(jumpSound);
                break;

            case "slide":
                audioSrc.PlayOneShot(slideSound);
                break;

            case "sword":
                audioSrc.PlayOneShot(attackSound);
                break;

            case "coincollect":
                audioSrc.PlayOneShot(collectcoinSound);
                break;

            case "boop":
                audioSrc.PlayOneShot(pressbuttonSound);
                break;

            case "hurtSound":
                audioSrc.PlayOneShot(gethitSound);
                break;

            case "deathsound":
                audioSrc.PlayOneShot(dieSound);
                break;
        }
    }
}

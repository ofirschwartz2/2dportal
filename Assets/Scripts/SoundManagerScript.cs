using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip playerDeadSound, jumpSound, shootSound, redLazerSound, blueLazerSound, portalSound, enterPortalSound;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        playerDeadSound = Resources.Load<AudioClip>("playerDead");
        jumpSound = Resources.Load<AudioClip>("jump");
        shootSound = Resources.Load<AudioClip>("shoot");
        redLazerSound = Resources.Load<AudioClip>("redLazer");
        blueLazerSound = Resources.Load<AudioClip>("blueLazer");
        portalSound = Resources.Load<AudioClip>("portal");
        enterPortalSound = Resources.Load<AudioClip>("enterPortal");

        audioSrc = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound (string clip) {
        switch (clip) {
            case "playerDead":
                audioSrc.PlayOneShot(playerDeadSound);
                break;
            case "jump":
                audioSrc.PlayOneShot(jumpSound);
                break;
            case "shoot":
                audioSrc.PlayOneShot(shootSound);
                break;
            case "redLazer":
                audioSrc.PlayOneShot(redLazerSound);
                break;
            case "blueLazer":
                audioSrc.PlayOneShot(blueLazerSound);
                break;
            case "portal":
                audioSrc.PlayOneShot(portalSound);
                break;
            case "enterPortal":
                audioSrc.PlayOneShot(enterPortalSound);
                break;  
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class PortalGun : MonoBehaviour
{
    public ShootingController shootingControllerScript;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Caveman") {
            shootingControllerScript.armed = true;
        }
    }
}

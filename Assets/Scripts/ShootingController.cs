using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public GameObject portalShoot;
    public float shotSpeed;
    private Vector2 shotDirection;

    // Start is called before the first frame update
    void Start()
    {
        shotDirection = new Vector2(10, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            var instPortalShoot = Instantiate(portalShoot, transform.position, Quaternion.identity);

            var instPortalShootRb = instPortalShoot.GetComponent<Rigidbody2D>();
            instPortalShootRb.AddForce(shotDirection * shotSpeed);
        }
    }

    
}

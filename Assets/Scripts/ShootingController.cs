using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public GameObject cavemanPortalGun;
    public GameObject portalShoot;
    public float shotSpeed;
    private Vector2 shotDirection;
    public bool armed;
    private SpriteRenderer portalgunsr;
    private Transform portalguntransform;


    // Start is called before the first frame update
    void Start()
    {
        shotDirection = new Vector2(10, 10);
        cavemanPortalGun.SetActive(false);
        portalgunsr = cavemanPortalGun.GetComponent<SpriteRenderer>();
        portalguntransform = cavemanPortalGun.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            portalgunsr.flipX = true;
            // Vector3 pos = portalguntransform.position;////////////
            // pos.x = pos.x - 1 ;//////////////////////////
            // portalguntransform.position = pos;//////////////
        }
        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
            portalgunsr.flipX = false;
        if (armed)
        {
            cavemanPortalGun.SetActive(true);
            if (Input.GetKeyDown(KeyCode.X))
            {
                var instPortalShoot = Instantiate(portalShoot, transform.position, Quaternion.identity);

                var instPortalShootRb = instPortalShoot.GetComponent<Rigidbody2D>();
                instPortalShootRb.AddForce(shotDirection * shotSpeed);
            }

        }
    }


}

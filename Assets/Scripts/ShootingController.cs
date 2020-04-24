using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public GameObject cavemanPortalGun;
    public GameObject portalShoot;
    public float shotSpeed;
    public bool armed;
    private SpriteRenderer portalGunSr;
    private Transform portalGunTransform;

    void Start()
    {
        cavemanPortalGun.SetActive(false);
        portalGunSr = cavemanPortalGun.GetComponent<SpriteRenderer>();
        portalGunTransform = cavemanPortalGun.GetComponent<Transform>();
    }

    void Update()
    {
        if (armed)
        {
            cavemanPortalGun.SetActive(true);
        }
        else
        {
            cavemanPortalGun.SetActive(false);
        }
        Shooting();
        PositioningGun();
        AimGun();
    }

    void PositioningGun()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            if (!portalGunSr.flipX)
            {
                portalGunTransform.eulerAngles = new Vector3(
                    0,
                    0,
                    portalGunTransform.eulerAngles.z * -1
                );
            }
            portalGunSr.flipX = true;
            Vector3 pos = transform.position;
            pos.x -= 0.18f;
            pos.y -= 0.1f;
            portalGunTransform.position = pos;

        }
        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            if (portalGunSr.flipX)
            {
                portalGunTransform.eulerAngles = new Vector3(
                    0,
                    0,
                    portalGunTransform.eulerAngles.z * -1
                );
            }
            portalGunSr.flipX = false;
            Vector3 pos = transform.position;
            pos.x += 0.18f;
            pos.y -= 0.1f;
            portalGunTransform.position = pos;
        }
    }

    void Shooting()
    {
        if (armed && Input.GetKeyDown(KeyCode.X))
        {
            var instPortalShoot = Instantiate(portalShoot, portalGunTransform.position, Quaternion.identity);
            var instPortalShootRb = instPortalShoot.GetComponent<Rigidbody2D>();
            Vector2 gunDirection = DegreeToVector2(portalGunTransform.eulerAngles.z);
            if (portalGunSr.flipX)
            {
                gunDirection.x *= -1;
                gunDirection.y *= -1;
            }
            instPortalShootRb.AddForce(gunDirection * shotSpeed);
        }
    }

    void AimGun()
    {
        if (Input.GetKey(KeyCode.A) && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)))
        {
            float gunRotation = Input.GetAxis("Vertical");
            if (portalGunSr.flipX) gunRotation *= -1;

            if (portalGunTransform.eulerAngles.z < 60 && gunRotation > 0 || portalGunTransform.eulerAngles.z > 300 && gunRotation < 0 || portalGunTransform.eulerAngles.z == 0 ||
                portalGunTransform.eulerAngles.z < 65 && gunRotation < 0 || portalGunTransform.eulerAngles.z > 295 && gunRotation > 0)
            {
                portalGunTransform.eulerAngles = new Vector3(
                    portalGunTransform.eulerAngles.x,
                    portalGunTransform.eulerAngles.y,
                    portalGunTransform.eulerAngles.z + gunRotation
                );
            }
        }
    }

    Vector2 DegreeToVector2(float degree)
    {
        return (Vector2)(Quaternion.Euler(0, 0, degree) * Vector2.right) * 10;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractionsController : MonoBehaviour
{
    public GameObject _portal1;
    public GameObject _portal2;
    public GameObject portalGun;
    public ShootingController shootingControllerScript;
    public Text _deadText, _winText;
    public bool _alive, _won;
    public Vector3 rebirthPosition;
    private Rigidbody2D _rb;
    private Sprite _deadSprite, _cavemanSprite;
    private SpriteRenderer _sr;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _alive = true;
        _won = false;
        _deadText.text = "";
        _winText.text = "";
        _deadSprite = Resources.Load<Sprite>("Caveman Dead");
        _cavemanSprite = Resources.Load<Sprite>("Caveman");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherGo = other.gameObject;
        if (otherGo.CompareTag("Spike") && (_rb.velocity.y < 0) || otherGo.CompareTag("VerticalBlueLaser") || otherGo.CompareTag("HorizontalBlueLaser") || otherGo.CompareTag("RedLaser"))
        {
            die();
        }
        if (otherGo.CompareTag("EndDoor"))
        {
            win();
        }
        if (otherGo.CompareTag("PortalGun"))
        {
            pickUpGun(other);
        }
    }


    private void die()
    {
        _alive = false;
        _sr.sprite = _deadSprite;
        shootingControllerScript.armed = false;
        _deadText.text = "DEAD";
        _portal1.transform.position = new Vector3(-15, -1, 0);
        _portal2.transform.position = new Vector3(-15, -1, 0);
        portalGun.SetActive(true);
        _rb.velocity = new Vector2(0, _rb.velocity.y);
        StartCoroutine(rebirth());
    }

    private IEnumerator rebirth()
    {
        yield return new WaitForSeconds(2f);
        _sr.sprite = _cavemanSprite;
        transform.position = rebirthPosition;
        _deadText.text = "";
        _alive = true;
    }
    private void win()
    {
        _won = true;
        _winText.text = "WIN";
        _rb.velocity = new Vector2(0, _rb.velocity.y);
    }

    private void pickUpGun(Collider2D gun)
    {
        gun.gameObject.SetActive(false);
    }
}

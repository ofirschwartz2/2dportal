using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractionsController : MonoBehaviour
{
    public GameObject portalGun;
    public ShootingController shootingControllerScript;
    public Text _deadText, _winText;
    public bool _alive, _won;
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
        if (other.gameObject.CompareTag("Spike") && (_rb.velocity.y < 0))
        {
            die();
        }
        if (other.gameObject.CompareTag("EndDoor"))
        {
            win();
        }
        if (other.gameObject.CompareTag("PortalGun"))
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
        portalGun.SetActive(true);
        _rb.velocity = new Vector2(0, _rb.velocity.y);
        StartCoroutine(rebirth());
    }

    private IEnumerator rebirth()
    {
        yield return new WaitForSeconds(1f);
        _sr.sprite = _cavemanSprite;
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

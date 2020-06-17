using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCreation : MonoBehaviour
{
    public int PortalCount;
    private GameObject _nextPortalToCreate;
    private GameObject _portal1;
    private GameObject _portal2;
    private Teleporter _portal;
    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _portal1 = GameObject.FindGameObjectWithTag("Portal1");
        _portal2 = GameObject.FindGameObjectWithTag("Portal2");
        _portal = FindObjectOfType<Teleporter>();
        PortalCount = 0;
    }

    void DecideNextPortal()
    {
        if (_portal.gameObject.CompareTag("Portal1") && _portal.lastCreated)
        {
            _nextPortalToCreate = _portal2;
            _portal.lastCreated = false;
        }
        else if (_portal.gameObject.CompareTag("Portal1") && !_portal.lastCreated)
        {
            _nextPortalToCreate = _portal1;
            _portal.lastCreated = true;
        }
        if (_portal.gameObject.CompareTag("Portal2") && _portal.lastCreated)
        {
            _nextPortalToCreate = _portal1;
            _portal.lastCreated = false;
        }
        else if (_portal.gameObject.CompareTag("Portal2") && !_portal.lastCreated)
        {
            _nextPortalToCreate = _portal2;
            _portal.lastCreated = true;
        }
    }

    private void DecideNextPortalRotation(ContactPoint2D contactPoint)
    {
        float z = 0;

        if (contactPoint.normal.x > 0)
            z = 180;
        else if (contactPoint.normal.x < 0)
            z = 0;
        else if (contactPoint.normal.y < 0)
            z = 90;
        else if (contactPoint.normal.y > 0)
            z = 270;

        _nextPortalToCreate.transform.rotation = Quaternion.Euler(0, 0, z);
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Ground"))
        {
            ContactPoint2D contactPoint = collisionInfo.GetContact(0);
            DecideNextPortal();
            _nextPortalToCreate.transform.position = contactPoint.point;
            DecideNextPortalRotation(contactPoint);
            PortalCount++;
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("RedLaser"))
            Destroy(gameObject);
        else if (other.gameObject.CompareTag("HorizontalBlueLaser"))
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * -1);
        }
        else if (other.gameObject.CompareTag("VerticalBlueLaser"))
        {
            _rb.velocity = new Vector2(_rb.velocity.x * -1, _rb.velocity.y);
        }
        else if (other.gameObject.CompareTag("DiagonalBlueLaser"))
        {
            if(_rb.velocity.y > 2)
                _rb.velocity = new Vector2(3, 0);
            if (_rb.velocity.x < -2)
                _rb.velocity = new Vector2(0, -3);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCreation : MonoBehaviour
{
    public int portalCount;
    private GameObject _nextPortalToCreate;
    private GameObject _portal1;
    private GameObject _portal2;
    private Teleporter _portal;

    // Start is called before the first frame update
    void Start()
    {
        _portal1 = GameObject.FindGameObjectWithTag("Portal1");
        _portal2 = GameObject.FindGameObjectWithTag("Portal2");
        _portal = FindObjectOfType<Teleporter>();
        portalCount = 0;
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
            portalCount++;
            Destroy(gameObject);
        }
    }

  
}

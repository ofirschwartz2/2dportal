using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public TimeSpan RestartTeleporterTimeSpan;
    public bool lastCreated;
    public double bottomBorder, rightBorder, topBorder, leftBorder;
    private Transform _portal1Transform;
    private Transform _portal2Transform;
    private PlayerMovementController _player;

    void Start()
    {
        _player = FindObjectOfType<PlayerMovementController>();
        RestartTeleporterTimeSpan = new TimeSpan(2000000);
        lastCreated = gameObject.CompareTag("Portal2");
    }

    void Update()
    {
        _portal1Transform = GameObject.FindGameObjectWithTag("Portal1").transform;
        _portal2Transform = GameObject.FindGameObjectWithTag("Portal2").transform;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Caveman") && IsPortalsInLevel() && DateTime.Now - _player.PortalEnterTime > RestartTeleporterTimeSpan)
        {
            _player.PortalEnterTime = DateTime.Now;
            Transform exitPortalTransform;
            Transform enterPortalTransform;
            if (gameObject.tag == "Portal1")
            {
                enterPortalTransform = _portal1Transform;
                exitPortalTransform = _portal2Transform;
            }
            else
            {
                enterPortalTransform = _portal2Transform;
                exitPortalTransform = _portal1Transform;
            }

            other.gameObject.transform.position = exitPortalTransform.position;

            VelocityChange(enterPortalTransform, exitPortalTransform, other.gameObject.GetComponent<Rigidbody2D>());
        }
    }

    private void VelocityChange(Transform enterPortalTransform, Transform exitPortalTransform, Rigidbody2D playerRb)
    {
        if (enterPortalTransform.eulerAngles.z == 180.0)
        {
            if (exitPortalTransform.eulerAngles.z == 180.0)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x * -1, playerRb.velocity.y);
            }
            else if (exitPortalTransform.eulerAngles.z == 270.0)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.y, playerRb.velocity.x * -1);
            }
            else if (exitPortalTransform.eulerAngles.z == 90.0)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.y, playerRb.velocity.x);
            }
        }
        else if (enterPortalTransform.eulerAngles.z == 0.0)
        {
            if (exitPortalTransform.eulerAngles.z == 0.0)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x * -1, playerRb.velocity.y);
            }
            else if (exitPortalTransform.eulerAngles.z == 270.0)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.y * -1, playerRb.velocity.x);
            }
            else if (exitPortalTransform.eulerAngles.z == 90.0)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.y, playerRb.velocity.x * -1);
            }
        }
        else if (enterPortalTransform.eulerAngles.z == 90.0)
        {
            if (exitPortalTransform.eulerAngles.z == 0.0)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.y * -1, 0.6f);
            }
            else if (exitPortalTransform.eulerAngles.z == 180.0)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.y, 0.6f);
            }
            else if (exitPortalTransform.eulerAngles.z == 90.0)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, playerRb.velocity.y * -1);
            }
        }
        else if (enterPortalTransform.eulerAngles.z == 270.0)
        {
            if (exitPortalTransform.eulerAngles.z == 0.0)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.y, 0.6f);
            }
            else if (exitPortalTransform.eulerAngles.z == 180.0)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.y * -1, 0.6f);
            }
            else if (exitPortalTransform.eulerAngles.z == 270.0)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, playerRb.velocity.y * -1);
            }
        }
    }

    private bool IsPortalsInLevel()
    {
        bool ans = true;

        if (_portal1Transform.position.x < leftBorder || _portal2Transform.position.x < leftBorder) ans = false;
        if (_portal1Transform.position.x > rightBorder || _portal2Transform.position.x > rightBorder) ans = false;
        if (_portal1Transform.position.y < bottomBorder || _portal2Transform.position.y < bottomBorder) ans = false;
        if (_portal1Transform.position.y > topBorder || _portal2Transform.position.y > topBorder) ans = false;

        return ans;
    }
}

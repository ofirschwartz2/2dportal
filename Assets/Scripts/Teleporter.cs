using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public TimeSpan RestartTeleporterTimeSpan;
    public bool lastCreated;
    private Vector3 _portal1Pos;
    private Vector3 _portal2Pos;
    private PlayerMovementController _player;

    void Start()
    {
        _player = FindObjectOfType<PlayerMovementController>();
        RestartTeleporterTimeSpan = new TimeSpan(2000000);
        lastCreated = gameObject.CompareTag("Portal2");
    }

    void Update()
    {
        _portal1Pos = GameObject.FindGameObjectWithTag("Portal1").transform.position;
        _portal2Pos = GameObject.FindGameObjectWithTag("Portal2").transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        string myTag = gameObject.tag;
        
        if (other.gameObject.CompareTag("Caveman") && PortalsInLevel() && DateTime.Now - _player.PortalEnterTime > RestartTeleporterTimeSpan)
        {
            _player.PortalEnterTime = DateTime.Now;
            other.gameObject.transform.position = myTag == "Portal1" ? _portal2Pos : _portal1Pos;
        }
    }

    private bool PortalsInLevel()
    {
        bool ans = !(_portal1Pos.x < -8.1 || _portal2Pos.x < -8.1);

        if (_portal1Pos.x < -8.1 || _portal2Pos.x < -8.1) ans = false;
        if (_portal1Pos.x > 8.88 || _portal2Pos.x > 8.88) ans = false;
        if (_portal1Pos.y < -4.21 || _portal2Pos.y < -4.21) ans = false;
        if (_portal1Pos.y > 4.22 || _portal2Pos.y > 4.22) ans = false;

        return ans;
    }
}

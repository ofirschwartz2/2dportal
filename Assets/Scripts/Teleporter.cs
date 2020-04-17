using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public TimeSpan RestartTeleporterTimeSpan;
    private Vector3 _portal1Pos;
    private Vector3 _portal2Pos;
    private PlayerMovementController _player;

    // Start is called before the first frame update
    void Start()
    {
        _portal1Pos = GameObject.FindGameObjectWithTag("Portal1").transform.position;
        _portal2Pos = GameObject.FindGameObjectWithTag("Portal2").transform.position;
        _player = FindObjectOfType<PlayerMovementController>();
        RestartTeleporterTimeSpan = new TimeSpan(2000000);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        string myTag = gameObject.tag;

        if (other.gameObject.CompareTag("Caveman") && DateTime.Now - _player.PortalEnterTime > RestartTeleporterTimeSpan)
        {
            _player.PortalEnterTime = DateTime.Now;
            other.gameObject.transform.position = myTag == "Portal1" ? _portal2Pos : _portal1Pos;
        }
    }
}

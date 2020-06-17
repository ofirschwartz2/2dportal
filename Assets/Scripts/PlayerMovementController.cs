using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float Speed;
    public float JumpSpeed;
    public float ClimbSpeed;
    public LayerMask ClimbObjects;
    public DateTime PortalEnterTime;
    public bool _climbing;
    public Rigidbody2D _rb;
    private float _originalGravityScale;
    private SpriteRenderer _sr;
    private Sprite _cavemanSprite, _climbingSprite, _lookUpSprite, _lookDownSprite;
    private PlayerInteractionsController _playerInteractions;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _originalGravityScale = _rb.gravityScale;
        _sr = GetComponent<SpriteRenderer>();
        _climbing = false;
        _cavemanSprite = Resources.Load<Sprite>("Caveman");
        _climbingSprite = Resources.Load<Sprite>("Caveman Climbing");
        _lookUpSprite = Resources.Load<Sprite>("CavemanLookUp");
        _lookDownSprite = Resources.Load<Sprite>("CavemanLookDown");
        PortalEnterTime = DateTime.MinValue;
        _playerInteractions = FindObjectOfType<PlayerInteractionsController>();
    }

    void Update()
    {
        LookController();
        MovementController();
        JumpController();
        ClimbDetector();
    }

    private void LookController()
    {
        if (_playerInteractions._alive && !_playerInteractions._won)
        {
            //flips:
            if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
                _sr.flipX = true;
            if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
                _sr.flipX = false;
            
            //everything else:
            if(_climbing)
                _sr.sprite = _climbingSprite;
            else if (_rb.velocity.y == 0 && _rb.velocity.x == 0 && !Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
                _sr.sprite = _lookUpSprite;
            else if (_rb.velocity.y == 0 && _rb.velocity.x == 0 && !Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
                _sr.sprite = _lookDownSprite;
            else
                _sr.sprite = _cavemanSprite;
        }
    }

    private void MovementController()
    {
        if (_playerInteractions._alive && !_playerInteractions._won)
        {
            
            if (!_climbing)
            {
                float horizontalMovement = Input.GetAxis("Horizontal") * Speed;
                if (_rb.velocity.y == 0)
                    _rb.velocity = new Vector2(horizontalMovement , _rb.velocity.y);
                else
                    _rb.velocity = new Vector2(horizontalMovement * 0.1f + _rb.velocity.x * 0.93f, _rb.velocity.y);
            }
            if (_climbing)
            {
                float verticalMovement = Input.GetAxis("Vertical") * ClimbSpeed;
                _rb.velocity = new Vector2(_rb.velocity.x, verticalMovement);
            }
        }
    }

    private void JumpController()
    {
        if (_playerInteractions._alive && !_playerInteractions._won && Input.GetKeyDown(KeyCode.Z) && _rb.velocity.y == 0)
        {
            _climbing = false;
            _sr.sprite = _cavemanSprite;
            _rb.AddForce(Vector2.up * JumpSpeed, ForceMode2D.Impulse);
        }
    }

    private void ClimbDetector()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, 2, ClimbObjects);

        if (hitInfo.collider != null)
        {
            if (!_climbing)
            {
                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
                {
                    _climbing = true;
                    _rb.velocity = Vector2.zero;
                    _rb.gravityScale = 0;
                }
                else
                    _rb.gravityScale = _originalGravityScale;
            }
            else
            {
                if (Input.GetKey(KeyCode.A))
                {
                    _rb.velocity = Vector2.zero;
                }
            }
        }
        else
        {
            _climbing = false;
            _rb.gravityScale = _originalGravityScale;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementController : MonoBehaviour
{
    public float Speed;
    public float JumpSpeed;
    public float ClimbSpeed;
    public Text _deadText;
    public Text _winText;
    public LayerMask ClimbObjects;
    private float _originalGravityScale;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private bool _alive, _won, _climbing;
    private Sprite _cavemanSprite, _climbingSprite, _deadSprite;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _originalGravityScale = _rb.gravityScale;
        _sr = GetComponent<SpriteRenderer>();
        _alive = true;
        _won = false;
        _climbing = false;
        _cavemanSprite = Resources.Load<Sprite>("Caveman");
        _climbingSprite = Resources.Load<Sprite>("Caveman Climbing2");
        _deadSprite = Resources.Load<Sprite>("Caveman Dead");
        _deadText.text = "";
        _winText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        LookController();
        MovementController();
        JumpController();
        ClimbDetector();
    }

    private void LookController()
    {
        if (_alive && !_won)
        {
            if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
                _sr.flipX = true;
            if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
                _sr.flipX = false;
        }
    }

    private void MovementController()
    {
        if (_alive && !_won)
        {
            if (!_climbing)
            {
                float horizontalMovement = Input.GetAxis("Horizontal") * Speed;
                _rb.velocity = new Vector2(horizontalMovement, _rb.velocity.y);
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
        if (_alive && !_won && Input.GetKeyDown(KeyCode.Z) && _rb.velocity.y == 0)
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
                    _sr.sprite = _climbingSprite;
                    _climbing = true;
                    _rb.velocity = Vector2.zero;
                    _rb.gravityScale = 0;
                }
                else
                    _rb.gravityScale = _originalGravityScale;
            }
        }
        else
        {
            _climbing = false;
            _rb.gravityScale = _originalGravityScale;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Spike") &&  (_rb.velocity.y < 0) )
        {
            _alive = false;
            _sr.sprite = _deadSprite;
            _deadText.text = "DEAD";
        }
        if (other.gameObject.CompareTag("EndDoor"))
        {
            _won = true;
            _winText.text = "WIN";
        }
    }

}

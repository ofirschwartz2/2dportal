using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject player;
    private Vector3 offset, above, below;
    private PlayerMovementController _playerMovement;
    private float pressTime;
    private bool upPressed, downPressed;

    void Start() {
        offset = transform.position - player.transform.position;
        above = new Vector3(0, 0.8f, 0);
        below = new Vector3(0, -0.8f, 0);
        _playerMovement = FindObjectOfType<PlayerMovementController>();
        pressTime = Time.time;
        upPressed = false;
        downPressed = false;
    }

    void LateUpdate() {
        if (!_playerMovement._climbing && _playerMovement._rb.velocity.y == 0 && _playerMovement._rb.velocity.x == 0 &&
            !Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            if (!upPressed)
                pressTime = Time.time + 0.5f;
            upPressed = true;
            downPressed = false;
        }
        else if (!_playerMovement._climbing && _playerMovement._rb.velocity.y == 0 && _playerMovement._rb.velocity.x == 0 &&
                 !Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
        {
            if (!downPressed)
                pressTime = Time.time + 0.5f;
            downPressed = true;
            upPressed = false;
        }
        else
        {
            downPressed = false;
            upPressed = false;
            transform.position = player.transform.position + offset;
        }

        if(upPressed && Time.time >= pressTime)
            transform.position = player.transform.position + offset + above;
        if(downPressed && Time.time >= pressTime)
            transform.position = player.transform.position + offset + below;

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}

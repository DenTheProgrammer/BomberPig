using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour, IPunObservable
{
    private Player player; 
    [SerializeField]
    private float movementSpeed = 1f;
    private Joystick joystick;
    private Transform gridStart;
    private Vector3 gridYdirection;
    private Rigidbody2D rb;
    private PhotonView view;
    private MovementDirection movementDirection;
    private SpriteRenderer spriteRenderer;
    public Sprite[] directions;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        joystick = FindObjectOfType<Joystick>();
        gridStart = GameObject.Find("gridStart").transform;
        rb = GetComponentInChildren<Rigidbody2D>();
        gridYdirection = gridStart.up.normalized;
        player = GetComponent<Player>();
        view = GetComponent<PhotonView>();
        movementDirection = MovementDirection.Right;
    }


    

    private void UpdateSprite()
    {
        switch (movementDirection)
        {
            case MovementDirection.Up:
                spriteRenderer.sprite = directions[0];
                break;
            case MovementDirection.Down:
                spriteRenderer.sprite = directions[1];
                break;
            case MovementDirection.Left:
                spriteRenderer.sprite = directions[2];
                break;
            case MovementDirection.Right:
                spriteRenderer.sprite = directions[3];
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSprite();
        if (!view.IsMine)//only local's player controls work
            return;
        //Keyboard Controls
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddRelativeForce(new Vector3(-movementSpeed * Time.deltaTime, 0), ForceMode2D.Impulse);
            movementDirection = MovementDirection.Left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddRelativeForce(new Vector3(movementSpeed * Time.deltaTime, 0), ForceMode2D.Impulse);
            movementDirection = MovementDirection.Right;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            rb.AddRelativeForce(gridYdirection * Time.deltaTime * -movementSpeed, ForceMode2D.Impulse);
            movementDirection = MovementDirection.Up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.AddRelativeForce(gridYdirection * Time.deltaTime * movementSpeed, ForceMode2D.Impulse);
            movementDirection = MovementDirection.Down;
        }
        if (Input.GetKeyDown(KeyCode.Space))
            player.PlaceBomb();
        //Mobile Controls
        /*
         * rb.AddRelativeForce(new Vector3(joystick.Horizontal * movementSpeed * Time.deltaTime, 0), ForceMode2D.Impulse);
         * rb.AddRelativeForce(gridYdirection * -joystick.Vertical * movementSpeed * Time.deltaTime, ForceMode2D.Impulse);
        */
        rb.AddRelativeForce(joystick.Direction * movementSpeed * Time.deltaTime, ForceMode2D.Impulse);//Equal speed in any direction this way
        JoystickCalculateDirection();

    }

    private void JoystickCalculateDirection()
    {
        if (joystick.Direction.magnitude > 0)//movement happens
        {
            float maxMovementValue = 0;
            MovementDirection maxMovementDirection = MovementDirection.Right;

            if (joystick.Direction.x > 0)//right movement value   etc.
            {
                if (joystick.Direction.x > maxMovementValue)
                {
                    maxMovementValue = joystick.Direction.x;
                    maxMovementDirection = MovementDirection.Right;
                }
            }
            if (joystick.Direction.x < 0)//left
            {
                if (Mathf.Abs(joystick.Direction.x) > maxMovementValue)
                {
                    maxMovementValue = Mathf.Abs(joystick.Direction.x);
                    maxMovementDirection = MovementDirection.Left;
                }
            }
            if (joystick.Direction.y > 0)//up
            {
                if (joystick.Direction.y > maxMovementValue)
                {
                    maxMovementValue = joystick.Direction.y;
                    maxMovementDirection = MovementDirection.Up;
                }
            }
            if (joystick.Direction.y < 0)//down
            {
                if (Mathf.Abs(joystick.Direction.y) > maxMovementValue)
                {
                    maxMovementValue = Mathf.Abs(joystick.Direction.y);
                    maxMovementDirection = MovementDirection.Down;
                }
            }
            movementDirection = maxMovementDirection;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(movementDirection);
        }else
        {
            movementDirection = (MovementDirection)stream.ReceiveNext();
        }
    }
}
enum MovementDirection
{
    Up,
    Down,
    Left,
    Right
}
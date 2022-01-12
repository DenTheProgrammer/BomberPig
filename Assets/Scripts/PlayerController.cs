using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    private Player player; 
    [SerializeField]
    private float movementSpeed = 1f;
    private Joystick joystick;
    private Transform gridStart;
    private Vector3 gridYdirection;
    private Rigidbody2D rb;
    private PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        gridStart = GameObject.Find("gridStart").transform;
        rb = GetComponentInChildren<Rigidbody2D>();
        gridYdirection = gridStart.up.normalized;
        player = GetComponent<Player>();
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!view.IsMine)//only local's player controls work
            return;
        //Keyboard Controls
        if (Input.GetKey(KeyCode.A))
            rb.AddRelativeForce(new Vector3(-movementSpeed * Time.deltaTime, 0), ForceMode2D.Impulse);
        else if (Input.GetKey(KeyCode.D))
            rb.AddRelativeForce(new Vector3(movementSpeed * Time.deltaTime, 0), ForceMode2D.Impulse);
        else if (Input.GetKey(KeyCode.W))
            rb.AddRelativeForce(gridYdirection * Time.deltaTime * -movementSpeed, ForceMode2D.Impulse);
        else if (Input.GetKey(KeyCode.S))
            rb.AddRelativeForce(gridYdirection * Time.deltaTime * movementSpeed, ForceMode2D.Impulse);
        if (Input.GetKeyDown(KeyCode.Space))
            player.PlaceBomb();
        //Mobile Controls
        /*
         * rb.AddRelativeForce(new Vector3(joystick.Horizontal * movementSpeed * Time.deltaTime, 0), ForceMode2D.Impulse);
         * rb.AddRelativeForce(gridYdirection * -joystick.Vertical * movementSpeed * Time.deltaTime, ForceMode2D.Impulse);
        */
        rb.AddRelativeForce(joystick.Direction * movementSpeed * Time.deltaTime, ForceMode2D.Impulse);//Equal speed in any direction this way
        
    }
}

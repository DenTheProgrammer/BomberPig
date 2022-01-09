using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float movementSpeed = 1f;
    [SerializeField]
    private Transform gridStart;

    private Vector3 gridYdirection;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        gridYdirection = gridStart.up.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        /* if (Input.GetKey(KeyCode.A))
             transform.position -= new Vector3(movementSpeed * Time.deltaTime, 0);
         if (Input.GetKey(KeyCode.D))
             transform.position += new Vector3(movementSpeed * Time.deltaTime, 0);
         if (Input.GetKey(KeyCode.W))
             transform.position -= gridYdirection * Time.deltaTime * movementSpeed;
         if (Input.GetKey(KeyCode.S))
             transform.position += gridYdirection * Time.deltaTime * movementSpeed;*/

        if (Input.GetKey(KeyCode.A))
            rb.AddRelativeForce(new Vector3(-movementSpeed * Time.deltaTime, 0), ForceMode2D.Impulse);
        if (Input.GetKey(KeyCode.D))
            rb.AddRelativeForce(new Vector3(movementSpeed * Time.deltaTime, 0), ForceMode2D.Impulse);
        if (Input.GetKey(KeyCode.W))
            rb.AddRelativeForce(gridYdirection * Time.deltaTime * -movementSpeed, ForceMode2D.Impulse);
        if (Input.GetKey(KeyCode.S))
            rb.AddRelativeForce(gridYdirection * Time.deltaTime * movementSpeed, ForceMode2D.Impulse);
    }
}

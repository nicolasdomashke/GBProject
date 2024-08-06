using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private Transform cameraPos;
    private float speed = 10f;
    private float jumpSpeed = 7f;
    private Vector3 cameraOffset = new Vector3(0f, 2.25f, -4.9f);
    private Vector3 moveVector = Vector3.zero;
    private Rigidbody rb;
    private bool isGrounded = true;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //float moveX = Input.GetAxis("Horizontal");
        //float moveZ = Input.GetAxis("Vertical");
        //moveVector = new Vector3(moveX, 0f, moveZ);
        moveVector = Vector3.zero;
        if (Input.GetKey("w"))
        {
            moveVector += Vector3.forward;
        }
        if (Input.GetKey("a"))
        {
            moveVector += Vector3.left;
        }
        if (Input.GetKey("s"))
        {
            moveVector += Vector3.back;
        }
        if (Input.GetKey("d"))
        {
            moveVector += Vector3.right;
        }
        
        //rb.AddForce(moveVector.normalized * speed, ForceMode.Acceleration);
        if (Input.GetKey("space") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            isGrounded = false;
        }
        cameraPos.position = transform.position + cameraOffset;
    }

    private void FixedUpdate() 
    {
        rb.AddForce(moveVector.normalized * speed, ForceMode.Acceleration);
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision other) {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}

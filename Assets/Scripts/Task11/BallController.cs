using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private Transform cameraPos;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float jumpSpeed = 1f;
    private Vector3 cameraOffset = new Vector3(0f, 2.25f, -4.9f);
    private Rigidbody rb;
    private bool isGrounded = true;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 moveVector = Vector3.zero;
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
        rb.AddForce(moveVector.normalized * speed, ForceMode.Acceleration);
        if (Input.GetKey("space") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            isGrounded = false;
        }
        cameraPos.position = transform.position + cameraOffset;
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

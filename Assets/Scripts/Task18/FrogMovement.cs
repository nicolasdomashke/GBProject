using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private AnimationCurve animationGraph;
    [SerializeField] private float jumpOffset;
    [SerializeField] private Transform jumpSphere;
    [SerializeField] private LayerMask groundMask;
    private Rigidbody2D rigidBody2D;
    private Vector2 movementVector;
    private Shooter shooter;
    private SpriteRenderer spriteRenderer;
    public bool isGrounded = false;
    void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        shooter = GetComponent<Shooter>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(jumpSphere.position, jumpOffset, groundMask);
    }
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        rigidBody2D.velocity = new Vector2(animationGraph.Evaluate(moveX) * speed, rigidBody2D.velocity.y);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpForce);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            shooter.Shoot(!spriteRenderer.flipX);
        }
    }
}
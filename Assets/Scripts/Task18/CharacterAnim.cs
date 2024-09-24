using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnim : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody2D;
    private FrogMovement frogMovement;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        frogMovement = GetComponent<FrogMovement>();
    }
    void Update()
    {
        Vector2 movementVector = rigidBody2D.velocity;
        animator.SetFloat("horizontalSpeedAbs", Mathf.Abs(movementVector.x));
        animator.SetFloat("verticalSpeed", movementVector.y);
        animator.SetBool("grounded", frogMovement.isGrounded);
        if (movementVector.x > 0.01f)
        {
            spriteRenderer.flipX = false;
        }
        else if (movementVector.x < -0.01f)
        {
            spriteRenderer.flipX = true;
        }
        
    }
}

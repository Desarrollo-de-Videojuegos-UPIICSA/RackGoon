
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = .5f;
    private bool isFacingRight;
    private float flipDirection;
    public bool canMove = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   
    }


    private void FixedUpdate()
    {
        if (canMove)
        {
            horizontal = Input.GetAxisRaw("Horizontal");

            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        
        flipDirection = GetComponentInChildren<Rotation>().degrees <= 0 ? (GetComponentInChildren<Rotation>().degrees * -1) : GetComponentInChildren<Rotation>().degrees;
        Debug.Log(flipDirection);
        if (flipDirection > 90)
        {
            // girar el sprite hacia izquierda
        }

        if (flipDirection <= 90)
        {
            // girar el sprite hacia derecha
        }

    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlataform")
        {
            transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlataform")
        {
            transform.parent = null;

        }
    }
}

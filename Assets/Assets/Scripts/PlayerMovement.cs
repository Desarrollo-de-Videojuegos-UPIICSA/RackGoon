
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private float speed = 1f;
    private float recoilSpeed = 10f;
    private float flipDirection;
    private bool isShooting;
    private Vector3 RecoilDirection;
    public bool canMove = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   
    }


    private void FixedUpdate()
    {
        RecoilDirection = GetComponentInChildren<Rotation>().direction;
        isShooting = Input.GetMouseButton(0) ? true : false;
        
        if (canMove)
        {
            if (isShooting)
            {
                Recoil();
            }
            else
            {
                horizontal = Input.GetAxisRaw("Horizontal");
                rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            }
            
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

    private void Recoil()
    {
        horizontal = RecoilDirection.x * -1;
        vertical = RecoilDirection.y * -1;

        rb.velocity = new Vector2(horizontal * recoilSpeed, vertical * recoilSpeed);
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

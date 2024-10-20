
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private float speed = 1f;
    private float recoilSpeed_1 = 5f;
    private float recoilSpeed_2 = 40f;
    private float flipDirection;
    private bool isShooting;
    public float principal_fireRate = 0.2f;
    public float secundary_fireRate = 2.0f;
    private float nextFireTime = 0f;
    private Vector3 RecoilDirection;
    public bool canMove = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;

    public Animator anime;
    private string nowplayin="";

    


    private void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
    

    }


    private void FixedUpdate()
    {
        RecoilDirection = GetComponentInChildren<Rotation>().direction;
        isShooting = Input.GetMouseButton(0) || Input.GetMouseButton(1) ? true : false;

      

        if (canMove)
        {
            if (isShooting)
            {
                Recoil();
            }
            else
            {
                horizontal = Input.GetAxisRaw("Horizontal");// 

                if (horizontal != 0)
                {
                    Switchanime("Run");
                }
                else
                {

                    Switchanime("Default");
                }


                rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
                //if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.A)) { }
               

            }
            
        }
        
        flipDirection = GetComponentInChildren<Rotation>().degrees <= 0 ? (GetComponentInChildren<Rotation>().degrees * -1) : GetComponentInChildren<Rotation>().degrees;
       // Debug.Log(flipDirection);
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

        Switchanime("Arma");
        if (Input.GetMouseButton(0))
        {
            horizontal = RecoilDirection.x * -1;
            vertical = RecoilDirection.y * -1;

           // rb.velocity = new Vector2(horizontal * recoilSpeed_1, vertical * recoilSpeed_1);
            rb.AddForce(new Vector2(horizontal * recoilSpeed_1, vertical * recoilSpeed_1));

            nextFireTime = Time.time + principal_fireRate;
        }
        else
        {
            horizontal = RecoilDirection.x * -1;
            vertical = RecoilDirection.y * -1;

           // rb.velocity = new Vector2(horizontal * recoilSpeed_2, vertical * recoilSpeed_2);
            rb.AddForce(new Vector2(horizontal * recoilSpeed_2, vertical * recoilSpeed_1) );


            nextFireTime = Time.time + secundary_fireRate;
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
    private void Switchanime(string animated, float corosfade = 0.2f)
    {

        if (nowplayin != animated)
        {
            nowplayin = animated;

            //   anime.Play(animated);
            anime.CrossFade(animated, corosfade);

        }

    }

 }






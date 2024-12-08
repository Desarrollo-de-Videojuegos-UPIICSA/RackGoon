// ya no le muevan cabrones
// todo: - arreglar la animacion
// atte: Patto 
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float recoilSpeed_1 = 4f;
    public float recoilSpeed_2 = 15f;
    public bool canMove = true;
    public Vector2 boxSize;
    public float castDistance;

    private float horizontal;
    private float vertical;
    private float flipDirection;
    private bool isShooting = false;
    private Vector3 RecoilDirection;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private Weapon weapon;
    [SerializeField] private LayerMask groundLayer;
    
    private string nowPlayin;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        RecoilDirection = GetComponentInChildren<Rotation>().direction;
        flipDirection = GetComponentInChildren<Rotation>().degrees;
        
        FlipSprite(flipDirection);
        
        if(rb.velocity.x <= 0.1) SwitchAnimation("Default");
        
        if (canMove)
        {
            if ((Input.GetMouseButton(0) || Input.GetMouseButton(1)) && !isShooting)
            {
                StartCoroutine(Recoil());
            }
        }
    }

    public void FlipSprite(float flipDirection)
    {
        if (!(flipDirection > -90 && flipDirection < 90))
        {
            spriteRenderer.flipX = true;
            weapon.GetComponentInChildren<SpriteRenderer>().flipY = true;
        }
        else
        {
            spriteRenderer.flipX = false;
            weapon.GetComponentInChildren<SpriteRenderer>().flipY = false;
        }
    }

    IEnumerator Recoil()
    {
        isShooting = true;
        SwitchAnimation("Arma");
        if (Input.GetMouseButton(0))
        {
            horizontal = RecoilDirection.x * -1;
            vertical = RecoilDirection.y * -1;

            if(!IsMaxHeight())
            {
                rb.velocity = new Vector2(horizontal * recoilSpeed_1, vertical * recoilSpeed_1);
            }
            else
            {
                rb.velocity = new Vector2(horizontal * recoilSpeed_1, (float)((vertical * recoilSpeed_1)*.5));
            }
            
            yield return new WaitForSeconds(weapon.principal_fireRate);
            
            SwitchAnimation("Default");
            rb.velocity = new Vector2(0, rb.velocity.y);
            isShooting = false;
        }
        else
        {
            horizontal = RecoilDirection.x * -1;
            vertical = RecoilDirection.y * -1;

            rb.velocity = new Vector2(horizontal * recoilSpeed_2, vertical * recoilSpeed_2);

            yield return new WaitForSeconds(weapon.secundary_fireRate);
            
            SwitchAnimation("Default");
            rb.velocity = new Vector2(0, rb.velocity.y);
            isShooting = false;
        }
    }

    public bool IsMaxHeight()
    {
        return !Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position-transform.up * castDistance, boxSize);
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
    private void SwitchAnimation(string animated, float corosfade = 0.2f)
    {
        if (nowPlayin != animated)
        {
            nowPlayin = animated;

            animator.CrossFade(animated, corosfade);
        }
    }
 }
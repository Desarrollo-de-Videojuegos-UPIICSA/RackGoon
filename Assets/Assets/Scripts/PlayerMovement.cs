using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float recoilSpeed_1 = 8f;
    public float recoilSpeed_2 = 16f;
    public float decelerationRate = 0.75f;
    public float minVelocity = 0.1f; // Velocidad mínima antes de detenerse por completo
    public Vector2 knockbackForce;
    public bool canMove = true;
    public Vector2 boxSize;
    public float castDistance;

    private float horizontal;
    private float vertical;
    private float flipDirection;
    private Vector3 RecoilDirection;
    
    [SerializeField] private Weapon weapon;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private Animator animator;
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
        
        if(rb.velocity.x <= minVelocity) SwitchAnimation("Default");

        if (canMove && weapon.IsShooting())
        {
            StartCoroutine(Recoil());
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

    public void Knockback(Vector2 hitPoint)
    {
        rb.velocity = new Vector2(-knockbackForce.x * hitPoint.x, knockbackForce.y);
        StartCoroutine(Decelerate());
    }

    IEnumerator Recoil()
    {
        canMove = false;
        SwitchAnimation("Arma");
        if (weapon.isShootingPrimary)
        {
            horizontal = RecoilDirection.x * -1;
            vertical = RecoilDirection.y * -1;

            if(!IsMaxHeight())
            {
                rb.velocity = new Vector2(rb.velocity.x + (horizontal * recoilSpeed_1),
                    rb.velocity.y + (vertical * recoilSpeed_1));
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x + (horizontal * recoilSpeed_1),
                    (rb.velocity.y + (vertical * recoilSpeed_1)) * .5f);
            }
            yield return new WaitForSeconds(weapon.principal_fireRate);
            rb.velocity = new Vector2(0, rb.velocity.y);
            canMove = true;
        }
        else if(weapon.isShootingSecondary)
        {
            horizontal = RecoilDirection.x * -1;
            vertical = RecoilDirection.y * -1;

            rb.velocity = new Vector2(rb.velocity.x + (horizontal * recoilSpeed_2), rb.velocity.y + (vertical * recoilSpeed_2));
            
            yield return new WaitForSeconds(weapon.secundary_fireRate);
            canMove = true;
            StartCoroutine(Decelerate());
        }
        
    }
    
    IEnumerator Decelerate()
    {
        while (rb.velocity.magnitude > 0.1f)
        {
            rb.velocity = new Vector2(rb.velocity.x * decelerationRate, rb.velocity.y * decelerationRate);
            yield return new WaitForFixedUpdate();
        }
        rb.velocity = Vector2.zero;
        SwitchAnimation("Default");
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
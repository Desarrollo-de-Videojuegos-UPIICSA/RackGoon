using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinguino_emperador : MonoBehaviour
{
    public float speed = 5f;
    public float moveDistance = 3f;
    private Vector3 startPosition;
    private bool movingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            if (transform.position.x >= startPosition.x + moveDistance)
            {
                FlipSprite();
                movingRight = false;
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            if (transform.position.x <= startPosition.x - moveDistance)
            {
                FlipSprite();
                movingRight = true;
            }
        }
    }

    // Método para voltear el sprite
    void FlipSprite()
    {
        Vector3 newScale = transform.localScale;
        newScale.x *= -1; // Invierte el valor de X
        transform.localScale = newScale;
    }

    // Cambia de dirección al colisionar con algo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        FlipSprite();
        movingRight = !movingRight; // Cambia la dirección
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinguino_emperador : MonoBehaviour
{
    public float speed = 5f;
    public float moveDistance = 3f;
    private Vector3 startPosition;
    private bool movingRight = true;


    public Transform player_pos;
    private Rigidbody2D rigi;
    private Vector2 direction;
    public bool waitin = true;
    private bool move = false;

    public int HPPin;


    // Start is called before the first frame update
    private void Awake()
    {
        rigi = GetComponent<Rigidbody2D>();
        player_pos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (waitin)
        {
            direction = player_pos.transform.position - transform.position;
            moveto();
        }
        else {
            Move(); }
        
    }


    void moveto()
    {
      

            if (direction.magnitude <= 10 )
            {

              //  rigi.AddForce(new Vector2(direction.x, transform.position.y) * speed*50 * Time.deltaTime);
            transform.Translate(direction.normalized * speed*2 * Time.deltaTime);
            // rigi.AddForce(Vector2.down* Time.deltaTime * speed/10) ;

        }
            else { rigi.AddForce(Vector2.zero); }


        
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

        if (collision.GetContact(0).collider.tag == ("Bala"))
        {

            HPPin--;

            if (HPPin < 1)
            {
                Destroy(this.gameObject);


            }


        }

      


    }
}
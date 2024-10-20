using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinguino : MonoBehaviour
{
    public float speed = 5f;
    public float moveDistance = 3f; 
    private Vector3 startPosition;
    private bool movingRight = true;

    public float taveltime;
    public float left;

    public Transform player_pos;
    private Rigidbody2D rigi;
    private bool waitin = false;
    public int HPMuro;

    void Start()
    {
        rigi= GetComponent<Rigidbody2D>();  
        player_pos = GameObject.FindGameObjectWithTag("Player").transform;
        //startPosition = transform.position; 
       // StartCoroutine(MoveEnemy());
    }

    private void FixedUpdate()
    {

        if (waitin == false )
        {
            rigi.AddForce(new Vector2(player_pos.position.x - this.transform.position.x, this.transform.position.y).normalized * speed * left);

        }
       






        /*

        if (movingRight)
        {
            while (transform.position.x < startPosition.x + moveDistance)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
               // yield return null;
            }
            movingRight = false;
          //  yield return new WaitForSeconds(1f);
        }
        else
        {
            while (transform.position.x > startPosition.x - moveDistance)
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                //yield return null;
            }
            movingRight = true;
           // yield return new WaitForSeconds(1f);
        }
        */
    }

    private IEnumerator MoveEnemy()
    {
        waitin = true;
        yield return new WaitForSeconds(1f);
        waitin = false;
        left = left * -1;

    }

    // Cambia de dirección al colisionar con algo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        movingRight = !movingRight; // Cambia la dirección

        if (collision.GetContact(0).collider.tag == ("Bala"))
        {

            HPMuro--;

            if (HPMuro < 1)
            {
                Debug.Log("la pala a sido destruida");


                Destroy(this.gameObject);


            }


        }

    }


 




}
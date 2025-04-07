using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinoIA : MonoBehaviour
{

    public float speed = 5f;
    public Transform player_pos;
    private Rigidbody2D rigi;
    private Vector2 direction;
    public bool waitin = true;
    public int seerange = 100;
    public int HPBoss;
    private bool GoinLeft = true;
    private void Awake()
    {
        rigi = GetComponent<Rigidbody2D>();
       

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        player_pos = GameObject.FindGameObjectWithTag("Player").transform;

        if (HPBoss < 30)
        {
            phase1();
        }
        else
        {
            phase2();
        }



    }




    private void phase1()
    {

        if (GoinLeft)
        {

            transform.Translate(Vector2.left * speed * speed * Time.deltaTime);
        }
        else 
        {

            transform.Translate(Vector2.right * speed * speed * Time.deltaTime);

        }
        

    }
    private void phase2()
    {
        if (GoinLeft)
        {

            transform.Translate(Vector2.left * speed*1.5f * speed * Time.deltaTime);
        }
        else
        {

            transform.Translate(Vector2.right * speed *1.5f* speed * Time.deltaTime);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GoinLeft)
        {
            GoinLeft = false;
        }
        if(GoinLeft == false)
        {
            GoinLeft = true;
        }
    }


}

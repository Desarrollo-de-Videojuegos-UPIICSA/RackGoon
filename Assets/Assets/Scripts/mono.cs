using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class mono : MonoBehaviour
{
    public Transform player_pos;
    public float speed;
    public float distancia_frenado;
    public Transform punto_instancia;
    public GameObject bala;
    private float tiempo;
    public float tiempo_disparo; // Para controlar la frecuencia de disparo
    public float fuerza_salto; // Fuerza del salto}
    private Rigidbody2D rb;

    public Animator anime;
    private string nowplayin = "";

    public int HPMuro;




    void Start()
    {
       
        rb = GetComponent<Rigidbody2D>(); // Referencia al Rigidbody2D

    
        player_pos = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SaltarCadaTresSegundos()); // Inicia la coroutine para saltar
        anime = GetComponent<Animator>();

        Switchanime("burla");


    }

    void FixedUpdate()
    {
        //movimiento
        if (Vector2.Distance(transform.position, player_pos.position) > distancia_frenado)
        {
            transform.position = Vector2.MoveTowards(transform.position, player_pos.position, speed * Time.deltaTime);
        }

        if (Vector2.Distance(transform.position, player_pos.position) < distancia_frenado)

        {
            transform.position = Vector2.MoveTowards(transform.position, player_pos.position, -speed * Time.deltaTime);
        }

        if (Vector2.Distance(transform.position, player_pos.position) < distancia_frenado && Vector2.Distance(transform.position, player_pos.position) > distancia_frenado)
        {
            transform.position = transform.position;
        }

        //flip
        if (player_pos.position.x > this.transform.position.x)
        {
            this.transform.localScale = new Vector2(1, 1);
        }
        else
        {
            this.transform.localScale = new Vector2(-1, 1);
        }

        //Disparo
        tiempo += Time.deltaTime;
        if (tiempo >= 7)
        {
            Instantiate(bala, this.transform.position, Quaternion.identity);
            tiempo = 0;
        }


        // Movimiento hacia el jugador

        distancia_frenado = 1f;

        if (Vector2.Distance(transform.position, player_pos.position) > distancia_frenado)
        {
            transform.position = Vector2.MoveTowards(transform.position, player_pos.position, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player_pos.position) < distancia_frenado)
        {
            transform.position = Vector2.MoveTowards(transform.position, player_pos.position, -speed * Time.deltaTime);
        }

        // Flip del sprite
        if (player_pos.position.x > this.transform.position.x)
        {
            this.transform.localScale = new Vector2(1, 1);
        }
        else
        {
            this.transform.localScale = new Vector2(-1, 1);
        }

       


        // Movimiento
        if (Vector2.Distance(transform.position, player_pos.position) > distancia_frenado)
        {
            transform.position = Vector2.MoveTowards(transform.position, player_pos.position, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player_pos.position) < distancia_frenado)
        {
            transform.position = Vector2.MoveTowards(transform.position, player_pos.position, -speed * Time.deltaTime);
        }

   

    }

    IEnumerator SaltarCadaTresSegundos()
    {
        while (true) // Bucle infinito
        {
            yield return new WaitForSeconds(3f); // Espera 3 segundos
            if (DeboSaltar())
            {
                rb.AddForce(new Vector2(0, fuerza_salto), ForceMode2D.Impulse);
            }
        }
    }

    // Método para definir cuándo el enemigo debe saltar (ejemplo básico)
    bool DeboSaltar()
    {
        // Puedes ajustar esta lógica, por ejemplo, si el enemigo está muy cerca del jugador
        return Vector2.Distance(transform.position, player_pos.position) < distancia_frenado && Mathf.Abs(rb.velocity.y) < 0.01f;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {


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

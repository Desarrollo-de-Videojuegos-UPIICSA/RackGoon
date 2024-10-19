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
    public float fuerza_salto; // Fuerza del salto

    private Rigidbody2D rb;


    void Start()
    {
        player_pos = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>(); // Referencia al Rigidbody2D

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
        if (tiempo >= 2)
        {
            Instantiate(bala, punto_instancia.position, Quaternion.identity);
            tiempo = 0;
        }


        // Movimiento hacia el jugador
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

        // Disparo
        tiempo += Time.deltaTime;
        if (tiempo >= tiempo_disparo)
        {
            Instantiate(bala, punto_instancia.position, Quaternion.identity);
            tiempo = 0;
        }

        // Salto
        if (DeboSaltar())
        {
            rb.AddForce(new Vector2(0, fuerza_salto), ForceMode2D.Impulse);
        }
    }

    // Método para definir cuándo el enemigo debe saltar (ejemplo básico)
    bool DeboSaltar()
    {
        // Puedes ajustar esta lógica, por ejemplo, si el enemigo está muy cerca del jugador
        return Vector2.Distance(transform.position, player_pos.position) < distancia_frenado && Mathf.Abs(rb.velocity.y) < 0.01f;
    }
}
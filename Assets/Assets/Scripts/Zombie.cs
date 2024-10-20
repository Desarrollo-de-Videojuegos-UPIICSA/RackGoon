using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Esteban: Quitare comportamiento de paralizar mapache

//El zombie ya cuenta con vida y movimiento, el unico fallo que tiene el bloqueo al jugador
public class Zombie : MonoBehaviour
{
    public float MoveSpeed = 2f; // Movimiento del enemigo
    public float MaxDisJugador = 1f; // Distancia máxima para detectar al jugador
    public Transform Player;
    //public float AttackDistance; // Distancia mínima para atacar al jugador
    //public float AttackDuration = 2f; // Duración del ataque en segundos
    //public float AttackCooldown = 5f; // Tiempo de espera entre ataques
    public int Vidazombie; //Es la vida del zombie
    public LayerMask CapaPlataforma;

    private Rigidbody2D rb; // Declara el Rigidbody2D
   // private bool isAttacking = false; // Indica si el zombie está atacando

    void Start()
    {
        // Obtiene el componente Rigidbody2D en el objeto actual
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

   private void FixedUpdate()
    {
        Vector2 EnemyPos = transform.position;
        Vector2 PlayerPos = Player.position;
        float distancia = Vector2.Distance(EnemyPos, PlayerPos);
        transform.position = Vector2.MoveTowards(EnemyPos, PlayerPos, MoveSpeed * Time.deltaTime);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(EnemyPos, CapaPlataforma);

        bool enPlataforma = false;
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.transform != gameObject.transform)
            {
                enPlataforma = true;
                break;
            }
        }

        if (enPlataforma)
        {
            rb.gravityScale = 0;
            transform.Translate(Vector2.down * MoveSpeed * Time.deltaTime);
        }
        if(Vector2.Distance(EnemyPos,PlayerPos)< MaxDisJugador)
        {
            GetComponent<Animator>().SetTrigger("Atacar");
        }
        // if (distancia <= MaxDisJugador && !isAttacking)
        // {
        // Mueve el enemigo hacia el jugador solo si está lejos
        // if (distancia > AttackDistance)
        // {

        //Vector2 direction = (PlayerPos - EnemyPos).normalized; // Normaliza la dirección
        // rb.MovePosition(rb.position + direction * MoveSpeed * Time.deltaTime); // Mueve el enemigo

        // Rotación hacia el jugador (opcional)
        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //rb.rotation = angle; // Cambia la rotación del Rigidbody2D
        // }
    }

    //Esta funcion es la que tiene el fallo, cuando se acerca al jugador despues de unos segundos se buguea y aparte no le hace nada al Jugador Dx
   /* private IEnumerator AttackPlayer()
    {
        isAttacking = true; // Indica que el zombie está atacando

        // Aquí puedes agregar la lógica para "morder" al jugador, como quitar salud
        PlayerMovement playerMovement = Player.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.canMove = false; // Desactiva el movimiento del jugador
            // Puedes agregar un efecto de daño visual o sonoro aquí
        }

        yield return new WaitForSeconds(AttackDuration); // Espera 2 segundos

        if (playerMovement != null)
        {
            playerMovement.canMove = true; // Reactiva el movimiento del jugador
        }

        yield return new WaitForSeconds(AttackCooldown); // Espera 5 segundos antes de poder atacar de nuevo
        isAttacking = false; // Permite que el zombie ataque de nuevo
    }
   */

    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.GetContact(0).collider.tag == ("Bala"))
        {

            Vidazombie--;

            if (Vidazombie < 1)
            {
        
                Destroy(this.gameObject);


            }


        }


    }
}

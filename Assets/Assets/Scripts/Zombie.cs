using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float MoveSpeed; // Movimiento del enemigo
    public float MaxDisJugador; // Distancia máxima para detectar al jugador
    public Transform Player;
    public float AttackDistance; // Distancia mínima para atacar al jugador
    public float AttackDuration = 2f; // Duración del ataque en segundos
    public float AttackCooldown = 5f; // Tiempo de espera entre ataques

    private Rigidbody2D rb; // Declara el Rigidbody2D
    private bool isAttacking = false; // Indica si el zombie está atacando

    void Start()
    {
        // Obtiene el componente Rigidbody2D en el objeto actual
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 EnemyPos = transform.position;
        Vector2 PlayerPos = Player.position;
        float distancia = Vector2.Distance(EnemyPos, PlayerPos);

        if (distancia <= MaxDisJugador && !isAttacking)
        {
            // Mueve el enemigo hacia el jugador solo si está lejos
            if (distancia > AttackDistance)
            {
                Vector2 direction = (PlayerPos - EnemyPos).normalized; // Normaliza la dirección
                rb.MovePosition(rb.position + direction * MoveSpeed * Time.deltaTime); // Mueve el enemigo

                // Rotación hacia el jugador (opcional)
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                rb.rotation = angle; // Cambia la rotación del Rigidbody2D
            }
            else
            {
                // Comienza el ataque si el jugador está dentro de la distancia de ataque
                StartCoroutine(AttackPlayer());
            }
        }
    }

    private IEnumerator AttackPlayer()
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
}

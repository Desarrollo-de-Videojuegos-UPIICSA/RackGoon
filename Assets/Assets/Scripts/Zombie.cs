using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float MoveSpeed; // Movimiento del enemigo
    public float MaxDisJugador; // Distancia m�xima en la que localiza al jugador
    public Transform Player;
    public float ThrowForce; // Fuerza con la que se lanzar� al jugador
    public float DisMinJugador; // Distancia m�nima para lanzar al jugador

    private Rigidbody2D rb; // Declara el Rigidbody2D

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

        if (distancia <= MaxDisJugador)
        {
            Vector2 direction = (PlayerPos - EnemyPos).normalized; // Normaliza la direcci�n
            rb.MovePosition(rb.position + direction * MoveSpeed * Time.deltaTime); // Mueve el enemigo

            // Rotaci�n hacia el jugador (opcional)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle; // Cambia la rotaci�n del Rigidbody2D

            // Verifica si el enemigo est� lo suficientemente cerca para lanzar al jugador
            if (distancia < DisMinJugador)
            {
                ThrowPlayer(direction);
            }
        }
    }

    private void ThrowPlayer(Vector2 direction)
    {
        // Obtiene el Rigidbody2D del jugador
        Rigidbody2D playerRb = Player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            // Aplica una fuerza al jugador en la direcci�n opuesta al enemigo
            playerRb.AddForce(-direction * ThrowForce, ForceMode2D.Impulse);
        }
    }
}

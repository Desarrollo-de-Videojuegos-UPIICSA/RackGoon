using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Animator animator;
    private Rigidbody2D rb;

    public int maxLives = 2; // Máximo número de corazones (vidas)
    private int currentLives; // Vidas actuales del jugador
    public GameObject[] hearts; // Array de objetos UI que representan los corazones
    private Vector3 currentCheckpoint;
    public float controlLoseTimer = 2f;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Inicializar vidas
        currentLives = maxLives;
        UpdateHeartsUI();

        currentCheckpoint = transform.position;
    }

    public void TakeDamage(float damage)
    {
        // Reducir vida
        currentLives--;
        UpdateHeartsUI();

        if (currentLives <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage, Vector2 position)
    {
        // Reducir vida
        currentLives--;
        UpdateHeartsUI();

        if (currentLives <= 0)
        {
            Die();
        }
        else
        {
            // Reaccionar al daño
            StartCoroutine(LoseControl());
            StartCoroutine(DisableCollider());
            playerMovement.Knockback(position);
        }
    }

    private void UpdateHeartsUI()
    {
        // Actualizar la visualización de los corazones
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentLives)
            {
                hearts[i].SetActive(true); // Mostrar corazón si está dentro del número de vidas actuales
            }
            else
            {
                hearts[i].SetActive(false); // Ocultar corazón si está fuera de las vidas actuales
            }
        }
    }

    private void Die()
    {
        // Acción al morir
        Debug.Log("El jugador ha muerto.");
        StartCoroutine(Respawn(0.5f));

    }

    IEnumerator Respawn(float duration)
    {
        rb.simulated = false;
        rb.velocity = Vector2.zero;
        transform.localScale = Vector3.zero;
        yield return new WaitForSeconds(duration);
        currentLives = maxLives;
        transform.position = currentCheckpoint;
        transform.localScale = Vector3.one;
        rb.simulated = true;
    }

    private IEnumerator DisableCollider()
    {
        Physics2D.IgnoreLayerCollision(7, 8, true);
        yield return new WaitForSeconds(controlLoseTimer);
        Physics2D.IgnoreLayerCollision(7, 8, false);
    }

    private IEnumerator LoseControl()
    {
        playerMovement.canMove = false;
        yield return new WaitForSeconds(controlLoseTimer);
        playerMovement.canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Checkpoint"))
        {
            currentCheckpoint = collision.transform.position; // Guarda la posicion del checkopoint para el respawn
            collision.GetComponent<Collider2D>().enabled = false; // Desactiva el colider del Checkpoint
        }
    }
}

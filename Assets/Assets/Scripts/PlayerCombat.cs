using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Animator animator;

    public int maxLives = 2; // Máximo número de corazones (vidas)
    private int currentLives; // Vidas actuales del jugador
    public GameObject[] hearts; // Array de objetos UI que representan los corazones

    public float controlLoseTimer = 2f;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();

        // Inicializar vidas
        currentLives = maxLives;
        UpdateHeartsUI();
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
        playerMovement.canMove = false;
        // Puedes añadir una animación de muerte, reinicio de nivel, etc.
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    public float speed = 5f; 
    public Transform player;  
    public float returnSpeed = 3f; 
    private Vector3 initialPosition; 
    private bool returning = false; 

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (returning)
        {
            ReturnToEnemy();
        }
        else
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            
            if (Vector3.Distance(transform.position, player.position) < 0.5f)
            {
                returning = true;
            }
        }
    }

    void ReturnToEnemy()
    {
        Vector3 direction = (initialPosition - transform.position).normalized;
        transform.position += direction * returnSpeed * Time.deltaTime;

       
        if (Vector3.Distance(transform.position, initialPosition) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
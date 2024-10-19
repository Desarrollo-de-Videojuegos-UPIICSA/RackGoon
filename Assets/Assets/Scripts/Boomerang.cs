using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
<<<<<<< Updated upstream
    public float speed = 5f; 
    public Transform player;  
    public float returnSpeed = 3f; 
    private Vector3 initialPosition; 
=======
    public float speed = 10f;
    public float returnTime = 100f;
    public Transform player;
    private Vector3 launchDirection;
>>>>>>> Stashed changes
    private bool returning = false;
    private Rigidbody2D rb;

    void Start()
    {
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();   
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
<<<<<<< Updated upstream
        if (player != null)
=======
         returnTimer += Time.deltaTime;
         float t = returnTimer / returnTime;
       // float t = Time.deltaTime;
          transform.position = Vector3.Lerp(transform.position, player.position, t);

        if (t >=  100f)
>>>>>>> Stashed changes
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
        rb.bodyType = RigidbodyType2D.Kinematic;


        if (Vector3.Distance(transform.position, initialPosition) < 0.1f)
        {
            Destroy(gameObject);
        }

    }
}
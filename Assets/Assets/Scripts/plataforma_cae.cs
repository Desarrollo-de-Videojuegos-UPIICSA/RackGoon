using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plataforma_cae : MonoBehaviour
{
    public float fallDelay = 0.3f;
    public float shakeAmount = 5f;
    bool readyToShake = false;

    Rigidbody2D rb;
    Vector3 originalPos;
    Collider2D coli;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coli = GetComponent<Collider2D>();  
    }

    void FixedUpdate()
    {
        if (readyToShake)
        {
            Vector3 newPos = originalPos + Random.insideUnitSphere * (Time.deltaTime * shakeAmount);
            newPos.y = transform.position.y;
            newPos.z = transform.position.z;

            transform.position = newPos;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Falling(fallDelay));
            //  rb.constraints = RigidbodyConstraints2D.None;
            
        }
    }

    IEnumerator Falling(float delay)
    {
        originalPos = transform.position;
        yield return new WaitForSeconds(delay);
     

        readyToShake = true;
        yield return new WaitForSeconds(1.0f);
        rb.bodyType = RigidbodyType2D.Dynamic;
        coli.enabled = false;

      

    }
}
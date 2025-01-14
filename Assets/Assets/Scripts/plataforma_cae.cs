using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plataforma_cae : MonoBehaviour
{
    public float fallDelay = 0.3f;
    public float shakeAmount = 5f;
    public float respawnDelay = 3f;
    public bool readyToShake = false;
    
    public Rigidbody2D rb;
    public Vector3 originalPos;
    public Collider2D coli;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coli = GetComponent<Collider2D>();  
        originalPos = transform.position;
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
        }
    }

    IEnumerator Falling(float delay)
    {
        yield return new WaitForSeconds(delay);

        readyToShake = true;
        yield return new WaitForSeconds(1.0f);
        rb.bodyType = RigidbodyType2D.Dynamic; 
        coli.enabled = false; 
        yield return new WaitForSeconds(respawnDelay); 
        RespawnPlatform();
    }

    void RespawnPlatform()
    {
        readyToShake = false;
        rb.bodyType = RigidbodyType2D.Static; 
        rb.velocity = Vector2.zero; 
        transform.position = originalPos; 
        coli.enabled = true; 
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    Rigidbody m_Rigidbody;
    public float speed = 20f;
    public float lifeTime = 2f;

    // Declarar el evento
    //public delegate void BulletFiredHandler();
    //public static event BulletFiredHandler OnBulletFired;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        // Destruir la bala después de 'lifeTime' segundos para limpiar la escena
        m_Rigidbody = GetComponent<Rigidbody>();
        Destroy(gameObject, lifeTime);

        // Disparar el evento al crearse la bala
        // if (OnBulletFired != null)
        // {
        //     OnBulletFired(); // Llamada al evento
        // }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        rigidbody.MovePosition(transform.position + transform.right * speed * Time.fixedDeltaTime);
    }
} 

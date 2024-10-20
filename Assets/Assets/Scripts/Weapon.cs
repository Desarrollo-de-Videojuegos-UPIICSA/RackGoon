using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject actionpoint;
    public GameObject Bala;
    public float principal_fireRate = 0.2f;
    public float secundary_fireRate = 2.0f;// Tiempo entre cada disparo
    private float nextFireTime = 0f; // Controla cuándo puede disparar de nuevo
    private bool WeaponType;

    void OnEnable()
    {
        // Suscribirse al evento OnBulletFired
        //Bullet.OnBulletFired += BulletFiredEventHandler;
    }

    // Método que se llama cuando se dispara el evento
    void BulletFiredEventHandler()
    {
        Debug.Log("¡Una bala fue disparada!");
        // Aquí puedes agregar lógica adicional que ocurra cuando se dispara la bala
    }

    void Update()
    {
        // El botón izquierdo del mouse está presionado
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            // Metodo de disparo de proyectil metralleta
            ShootProjectile();
            nextFireTime = Time.time + principal_fireRate;
            WeaponType = true;
        }
        if (Input.GetMouseButton(1) && Time.time >= nextFireTime)
        {
            ShootProjectile();
            nextFireTime = Time.time + secundary_fireRate;
            WeaponType = false;
        }
    }

    void ShootProjectile()
    {
        if (WeaponType)
        {
            Debug.Log("Babb!!!!");
            GameObject projectile = Instantiate(Bala, actionpoint.transform.position, actionpoint.transform.rotation); 
        }
        else
        {
            GameObject projectile = Instantiate(Bala, actionpoint.transform.position, actionpoint.transform.rotation);
        }
        
    }

}

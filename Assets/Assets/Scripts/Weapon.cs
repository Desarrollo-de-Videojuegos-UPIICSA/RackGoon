using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject actionpoint;
    public GameObject Bala;
    public float fireRate = 0.2f; // Tiempo entre cada disparo
    private float nextFireTime = 0f; // Controla cuándo puede disparar de nuevo

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
            nextFireTime = Time.time + fireRate;
        }
    }

    void ShootProjectile()
    {
        Debug.Log("Babb!!!!");
        GameObject projectile = Instantiate(Bala, actionpoint.transform.position, actionpoint.transform.rotation);


    }

}

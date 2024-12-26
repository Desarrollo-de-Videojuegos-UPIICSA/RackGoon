using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public PlayerMovement playerMovement;
    
    public GameObject actionpoint;
    public GameObject Bala;
    public float principal_fireRate = 0.2f;
    public float secundary_fireRate = 1.0f; // Tiempo entre cada disparo
    public bool isShootingPrimary = false;
    public bool isShootingSecondary = false;
    
    void FixedUpdate()
    {
        if (playerMovement.canMove)
        {
            HandlePrimaryShooting();
            HandleSecondaryShooting();
        }
    }

    private void HandlePrimaryShooting()
    {
        if (Input.GetMouseButton(0) && !isShootingPrimary)
        {
            isShootingPrimary = true;
            StartCoroutine(ShootPrimaryProjectile());
        }
    }

    private void HandleSecondaryShooting()
    {
        if (Input.GetMouseButton(1) && !isShootingSecondary)
        {
            isShootingSecondary = true;
            StartCoroutine(ShootSecondaryProjectile());
        }
    }

    private IEnumerator ShootPrimaryProjectile()
    {
        Instantiate(Bala, actionpoint.transform.position, actionpoint.transform.rotation);
        yield return new WaitForSeconds(principal_fireRate);
        isShootingPrimary = false;
    }

    private IEnumerator ShootSecondaryProjectile()
    {
        Instantiate(Bala, actionpoint.transform.position, actionpoint.transform.rotation);
        yield return new WaitForSeconds(secundary_fireRate);
        isShootingSecondary = false;
    }

    public bool IsShooting()
    {
        return isShootingPrimary || isShootingSecondary;
    }
}
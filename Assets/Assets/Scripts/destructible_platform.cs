using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructible_platform: MonoBehaviour
{
    //verificar si la fuel el impacato de la bala 
    public int HPMuro;


    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.GetContact(0).collider.tag==("Bala" ))
        {

            HPMuro--;

            if (HPMuro < 1) {
                Debug.Log("la pala a sido destruida");


                Destroy(this.gameObject);


            }

         
        }
        

    }



}

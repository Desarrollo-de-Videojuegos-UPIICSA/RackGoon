using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Rotation : MonoBehaviour
{


    [Header("Mouse stuff ")]

    public Vector2 mousepost;
    public float degrees;



    [Header("Wepon caracteristics ")]

    public int WPNDMGPRIM;
    public int WPNDMGSECN;

    public Vector3 direction;
    public GameObject ACTNPoint;

    public int Attacktimes;
    public float attackTMOUT;

    public float MouseACT1CLDWN;
    public GameObject MouseACT1SPRT;
    public float MouseACT1SPD;

    public float MouseACT2CLDWN;
    public GameObject MouseACT2SPRT;
    public float MouseACT2SPD;

  


    private void FixedUpdate()
    {

        mousepost = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        direction = new Vector3(transform.position.x - ACTNPoint.transform.position.x,
                                transform.position.y - ACTNPoint.transform.position.y).normalized * -1;
        degrees = Mathf.Atan2(mousepost.y, mousepost.x) * Mathf.Rad2Deg;
        transform.eulerAngles = Vector3.forward * degrees;

    }


  


}

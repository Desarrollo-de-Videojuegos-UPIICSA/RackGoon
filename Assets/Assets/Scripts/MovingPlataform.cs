using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class MovingPlataform : MonoBehaviour
{
    public GameObject PlataformToMove;
    
    public Transform StartPoint;
    public Transform EndPoint;

    public float Velocity;
    
    private Vector3 MoveTo;
    // Start is called before the first frame update
    void Start()
    {
        
        MoveTo = EndPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        PlataformToMove.transform.position =
            Vector3.MoveTowards(PlataformToMove.transform.position, MoveTo, Velocity * Time.deltaTime);

        if (PlataformToMove.transform.position == EndPoint.position)
        {
            MoveTo = StartPoint.position;
        }
        if (PlataformToMove.transform.position == StartPoint.position)
        {
            MoveTo = EndPoint.position;
        }
    }
}

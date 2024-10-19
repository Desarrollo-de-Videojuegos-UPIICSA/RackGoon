using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinguino_emperador : MonoBehaviour
{
    public float speed = 5f;
    public float moveDistance = 3f;
    private Vector3 startPosition;
    private bool movingRight = true;
    
    // Start is called before the first frame update
    void Start()
    {
    startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            if (transform.position.x >= startPosition.x + moveDistance)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            if (transform.position.x <= startPosition.x - moveDistance)
            {
                movingRight = true;
            }
        }
    }
}

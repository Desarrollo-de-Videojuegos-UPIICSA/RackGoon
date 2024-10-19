using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinguino : MonoBehaviour
{
    public float speed = 5f;
    public float moveDistance = 3f; 
    private Vector3 startPosition;
    private bool movingRight = true;

    void Start()
    {
        startPosition = transform.position; 
        StartCoroutine(MoveEnemy());
    }

    private IEnumerator MoveEnemy()
    {
        while (true)
        {
            if (movingRight)
            {
                while (transform.position.x < startPosition.x + moveDistance)
                {
                    transform.Translate(Vector2.right * speed * Time.deltaTime);
                    yield return null;
                }
                movingRight = false; 
                yield return new WaitForSeconds(1f); 
            }
            else
            {
                while (transform.position.x > startPosition.x - moveDistance)
                {
                    transform.Translate(Vector2.left * speed * Time.deltaTime);
                    yield return null;
                }
                movingRight = true; 
                yield return new WaitForSeconds(1f); 
            }
        }
    }
}
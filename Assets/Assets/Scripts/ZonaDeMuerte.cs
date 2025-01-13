using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZonaDeMuerte : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.GetContact(0).collider.tag == "Player")
        {
            // Obtiene la escena actual
            Scene currentScene = SceneManager.GetActiveScene();

            // Recarga la escena actual
            SceneManager.LoadScene(currentScene.name);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menuinicial : MonoBehaviour
{

    public void BotonPlay()
    {

        // Debug.Log("clicking game time");
        SceneManager.LoadScene(2);

    }

    public void Botonexit()
    {
        Debug.Log("Salir");
        Application.Quit();
    }

}

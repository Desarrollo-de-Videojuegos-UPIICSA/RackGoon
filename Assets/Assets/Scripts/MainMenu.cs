using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class MainMenu : MonoBehaviour
{
    public GameObject Title;
    public Transform EndPoint;
    public GameObject Logo;
    public Canvas Buttons;
    public Image FlashPanel;
    public AudioSource soundEffectSource; // Fuente de audio para efecto de escopeta
    public AudioSource musicSource; // Fuente de audio para la música

    private bool isAnimating;
    private float Velocity = 100;
    
    private void Start()
    {
        Logo.SetActive(false);
        Buttons.enabled = false;
        var color = FlashPanel.color;
        color.a = 0.0f;
        FlashPanel.color = color;

        isAnimating = true;
    }

    public void FixedUpdate()
    {
        if (isAnimating)
        {
            StartCoroutine(MenuAnimation());
            isAnimating = false; // Para asegurarnos de que la corutina solo se inicie una vez
        }
    }

    IEnumerator MenuAnimation()
    {
        // Titulo cayendo
        yield return StartCoroutine(TitleFall());
        
        // Disparo de escopeta y fade in
        yield return StartCoroutine(FadeIn());
        
        // Fade out
        yield return StartCoroutine(FadeOut());
        
        FlashPanel.gameObject.SetActive(false);
    }

    IEnumerator TitleFall()
    {
        while (Title.transform.position != EndPoint.position)
        {
            Title.transform.position = Vector3.MoveTowards(Title.transform.position, EndPoint.position, Velocity * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator FadeIn()
    {
        // Reproducir sonido antes del fading
        if (!soundEffectSource.isPlaying)
        {
            soundEffectSource.Play();
        }

        while (FlashPanel.color.a < 1)
        {
            var color = FlashPanel.color;
            color.a += Time.fixedDeltaTime;
            FlashPanel.color = color;
            yield return null;
        }

        Logo.SetActive(true);
        Buttons.enabled = true;
    }

    IEnumerator FadeOut()
    {
        while (FlashPanel.color.a > 0)
        {
            var color = FlashPanel.color;
            color.a -= Time.fixedDeltaTime;
            FlashPanel.color = color;
            yield return null;
        }

        yield return new WaitForSeconds(.5f);

        // Reproducir música después del fading
        if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

    public void BotonPlay()
    {
        // Debug.Log("clicking game time");
        SceneManager.LoadScene(1);
    }

    public void Botonexit()
    {
        Debug.Log("Salir");
        Application.Quit();
    }
}
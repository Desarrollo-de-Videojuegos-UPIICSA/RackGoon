using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Comic : MonoBehaviour
{
    public RectTransform comicImage; // Referencia al RectTransform de la imagen del cómic.
    public float zoomDuration = 2f;  // Duración de la animación de zoom.
    public float waitTime = 3f;      // Tiempo de espera entre cada zoom.

    // Posiciones y escalas para cada viñeta
    [System.Serializable]
    public class PanelZoom
    {
        public Vector2 position; // Posición de la viñeta (X, Y)
        public float scale;      // Escala para el zoom
    }

    public PanelZoom[] panels; // Array de posiciones y escalas

    void Start()
    {
        StartCoroutine(ZoomSequence());
    }

    IEnumerator ZoomSequence()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            // Llama a la animación para mover y hacer zoom
            yield return StartCoroutine(ZoomToPanel(panels[i]));

            // Espera un tiempo antes de ir al siguiente panel
            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator ZoomToPanel(PanelZoom panel)
    {
        Vector2 startPosition = comicImage.anchoredPosition;
        float startScale = comicImage.localScale.x;

        float elapsed = 0f;

        while (elapsed < zoomDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsed / zoomDuration);

            // Interpola la posición y escala
            comicImage.anchoredPosition = Vector2.Lerp(startPosition, panel.position, t);
            float scale = Mathf.Lerp(startScale, panel.scale, t);
            comicImage.localScale = new Vector3(scale, scale, 1);

            yield return null;
        }

        // Asegura que la posición y escala sean exactas
        comicImage.anchoredPosition = panel.position;
        comicImage.localScale = new Vector3(panel.scale, panel.scale, 1);
    }
}
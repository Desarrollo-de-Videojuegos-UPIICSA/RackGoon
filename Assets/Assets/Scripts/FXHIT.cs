using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXHIT : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Bala")) {
            audioSource.Play();
        }
    }
}

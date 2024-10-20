using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("Audio").GetComponent<MusicLoop>().StopMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

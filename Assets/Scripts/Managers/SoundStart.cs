using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundStart : MonoBehaviour
{
    public AudioSource start;
    void Start()
    {
        start.Play();
    }
}

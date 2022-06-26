using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Img : MonoBehaviour
{
    public bool isCaught = false;

    public SpriteRenderer sr;

    void Update()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = GameObject.Find("Player").GetComponent<SpriteRenderer>().sprite;
    }
}

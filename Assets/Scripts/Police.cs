using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : CharBase
{
    // Start is called before the first frame update
    void Start()
    {
        scale = Mathf.Abs(transform.localScale.x);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

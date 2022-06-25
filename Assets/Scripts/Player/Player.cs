using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharBase
{
    // Start is called before the first frame update
     void Start()
    {
        scale = Mathf.Abs(transform.localScale.x);   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * Time.deltaTime * spd);
            transform.localScale = new Vector3(1, 1, 1) * scale;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * Time.deltaTime * spd);
            transform.localScale = new Vector3(-1, 1, 1) * scale;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.down * Time.deltaTime * spd);
            transform.localScale = new Vector3(1, -1, 1) * scale;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * Time.deltaTime * spd);
            transform.localScale = new Vector3(1, 1, 1) * scale;
        }

    }
}

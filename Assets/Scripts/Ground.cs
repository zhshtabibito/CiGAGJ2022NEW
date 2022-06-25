using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = SpriteManager.Instance.RandomGround();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

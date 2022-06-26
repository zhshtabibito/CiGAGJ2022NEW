using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reverse : MonoBehaviour
{
    public GameObject img;
    private bool isUse = true;
    
    void Start()
    {
        img = Instantiate(img,null);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(GameObject.Find("Player").transform.position,gameObject.transform.position);

        RaycastHit2D hit1 = Physics2D.Raycast(transform.position,GameObject.Find("Player").transform.position-transform.position,distance,LayerMask.GetMask("coll"));
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position,img.transform.position-transform.position,distance,LayerMask.GetMask("coll"));

        Vector2 newVec = gameObject.transform.position*2-GameObject.Find("Player").transform.position;

        img.transform.position = newVec;

        if(isUse)
        {
            img.SetActive(true);
        }

        if(hit1.collider!=null||hit2.collider!=null)
        {
            img.SetActive(false);
        }
    }
}

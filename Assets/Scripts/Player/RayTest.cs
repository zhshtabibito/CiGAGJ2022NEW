using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoBehaviour
{
    public GameObject img;
    private bool isUse = false;
    void Start()
    {
        img = Instantiate(img,null);
    }
    void Update()
    {

        Vector2 vertical = gameObject.transform.GetChild(0).gameObject.transform.position-transform.position;
        Vector2 oldVec = GameObject.Find("Player").transform.position-transform.position;

        RaycastHit2D hit1 = Physics2D.Raycast(transform.position,GameObject.Find("Player").transform.position-transform.position,1000,LayerMask.GetMask("coll"));
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position,img.transform.position-transform.position,1000,LayerMask.GetMask("coll"));

        if(!isUse)
        {
            img.SetActive(true);
        }

        var x = oldVec.x;
        var y = oldVec.y;

        var newX = y+transform.position.x;
        var newY = x+transform.position.y;

        Vector2 newVec = new Vector2((float)newX,(float)newY);

        img.transform.position = newVec;

        /*if(gameObject.tag == "Mirror1"||gameObject.tag == "Mirror2");
        {
            img.transform.position = newVec;
        }

        if(gameObject.tag == "Mirror3"||gameObject.tag == "Mirror4");
        {
            img.transform.position = -newVec;
        }*/
        

        if(hit1.collider!=null||hit2.collider!=null)
        {
            img.SetActive(false);
        }
    }
}

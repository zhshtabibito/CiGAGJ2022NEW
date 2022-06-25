using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoBehaviour
{
    public float angle = 0;
    void Update()
    {

        Vector2 vertical = gameObject.transform.GetChild(0).gameObject.transform.position-transform.position;
        Vector2 oldVec = GameObject.Find("Player").transform.position-transform.position;

        RaycastHit2D hit1 = Physics2D.Raycast(transform.position,GameObject.Find("Player").transform.position-transform.position,100,LayerMask.GetMask("coll"));
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position,GameObject.Find("Player").transform.GetChild(0).transform.position-transform.position,100,LayerMask.GetMask("coll"));
        angle = Vector2.Angle(oldVec,vertical);

        GameObject.Find("Player").transform.GetChild(0).gameObject.SetActive(true);

        var x = oldVec.x;
        var y = oldVec.y;

        var newX = y+transform.position.x;
        var newY = x+transform.position.y;

        Vector2 newVec = new Vector2((float)newX,(float)newY);

        GameObject.Find("Player").transform.GetChild(0).gameObject.transform.position = newVec;

        if(hit1.collider!=null||hit2.collider!=null)
        {
            GameObject.Find("Player").transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}

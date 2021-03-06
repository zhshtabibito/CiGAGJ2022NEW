using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoBehaviour
{
    public GameObject img;
    public float timer = 0;
    public float disTime;

    void Start()
    {
        img = Instantiate(img,null);
        disTime = 5;
    }
    void Update()
    {

        Vector2 vertical = gameObject.transform.GetChild(0).gameObject.transform.position-transform.position;
        Vector2 oldVec = GameObject.Find("Player").transform.position-transform.position;

        float distance = Vector2.Distance(GameObject.Find("Player").transform.position,gameObject.transform.position);

        RaycastHit2D hit1 = Physics2D.Raycast(transform.position,GameObject.Find("Player").transform.position-transform.position,distance,LayerMask.GetMask("coll"));
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position,img.transform.position-transform.position,distance,LayerMask.GetMask("coll"));

        var x = oldVec.x;
        var y = oldVec.y;

        var newX = y+transform.position.x;
        var newY = x+transform.position.y;

        Vector2 newVec = new Vector2((float)newX,(float)newY);

        img.transform.position = newVec;
        
        if(img.GetComponent<Img>().isCaught)
        {
            timer += Time.deltaTime;
            if(timer>disTime)
            {
                timer = 0;
                img.GetComponent<Img>().isCaught = false;
            }
        }

        if(hit1.collider!=null||hit2.collider!=null||img.GetComponent<Img>().isCaught)
        {
            img.SetActive(false);
        }
        else
        {
            img.SetActive(true);
        }
    }
}

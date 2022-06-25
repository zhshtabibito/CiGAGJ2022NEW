using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    private List<GameObject> CollidingList;

    // Start is called before the first frame update
    void Start()
    {
        CollidingList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollidingList.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CollidingList.Remove(collision.gameObject);
    }

    // if wall return true
    public bool CheckWall()
    {
        foreach(GameObject obj in CollidingList)
        {
            if (obj.CompareTag("Wall"))
                return true;
        }
        return false;
    }

}

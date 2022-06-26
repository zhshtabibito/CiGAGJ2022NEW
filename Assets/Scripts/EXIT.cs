using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXIT : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && Player.Instance.prepared)
        {
            Debug.Log("Player EXIT");
            Time.timeScale = 0;
            PanelManager.Instance.Push(new NextLevelPanel());
        }
    }
}

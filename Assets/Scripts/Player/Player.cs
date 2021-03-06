using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharBase
{
    public static Player Instance;
    public bool prepared = false;
    private Vector3 BirthPos;

    // Start is called before the first frame update
     void Start()
    {
        Instance = this;
        scale = Mathf.Abs(transform.localScale.x);
        animator = GetComponent<Animator>();
        BirthPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !prepared)
        {
            GameObject.Find("LevelRoot").GetComponent<LevelInfo>().OnPrepared();
            PanelManager.Instance.Pop();
            // place menu button
            prepared = true;
            transform.position = BirthPos;
        }


        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * Time.deltaTime * spd);
            SetDir(1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * Time.deltaTime * spd);
            SetDir(2);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.down * Time.deltaTime * spd);
            SetDir(3);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * Time.deltaTime * spd);
            SetDir(4);
        }
        transform.localScale = dir * scale;
        DetectorPrefab.localScale = dir;
    }

    public  Vector3 GetPos()
    {
        return new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // if can place wall return true
    public int CheckWall()
    {
        bool d1 = Detectors[0].GetComponent<Detector>().CheckWall();
        bool d2 = Detectors[1].GetComponent<Detector>().CheckWall();
        bool d3 = Detectors[2].GetComponent<Detector>().CheckWall();
        bool d4 = Detectors[3].GetComponent<Detector>().CheckWall();

        Debug.Log($"{d1},{d2},{d3},{d4}");

        if (d1 && d2 && !d3 && !d4) // ????
            return 4;
        if (d2 && d3 && !d1 && !d4) // ????
            return 1;
        if (d3 && d4 && !d1 && !d2) // ????
            return 3;
        if (d1 && d4 && !d2 && !d3) // ????
            return 2;
        return 0;
    }







}

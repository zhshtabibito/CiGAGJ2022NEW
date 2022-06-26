using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelInfo : MonoBehaviour
{
    public int LevelID;
    public int MirrorNum = 1;

    private int MirrorLeft;
    private Transform numObj;
    private Transform player;
    private List<GameObject> MirrorList;

    public List<GameObject> PoliceList;
    public void OnPrepared()
    {
        foreach (GameObject p in PoliceList)
            p.GetComponent<Police>().OnPrepared();
    }



    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        MirrorLeft = MirrorNum;
        numObj = GameObject.Find("TextMirrorNum").transform;
        SetNum();

        player = Player.Instance.transform;

        MirrorList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (MirrorLeft < MirrorNum)
            {
                foreach (GameObject m in MirrorList)
                {
                    if (Mathf.Abs(m.transform.position.x - player.position.x) < 0.5f &&
                        Mathf.Abs(m.transform.position.y - player.position.y) < 0.5f)
                    {
                        MirrorList.Remove(m);
                        MirrorLeft++;
                        SetNum();
                        Object.Destroy(m);
                        return;
                    }
                }
            }
            if (MirrorLeft > 0)
            {
                int mid = player.GetComponent<Player>().CheckWall();
                if(mid>0)
                    SetMirror(mid);
            }
        }
    }

    private void SetNum()
    {
        numObj.GetComponent<TMP_Text>().text = $"X{MirrorLeft}";
    }

    private void SetMirror(int mid)
    {
        GameObject m = Object.Instantiate((GameObject)Resources.Load($"Prefabs/Mirror/Mirror{mid}"));
        m.transform.position = AllignPos();
        MirrorList.Add(m);
        MirrorLeft--;
        SetNum();
    }

    private Vector3 AllignPos()
    {
        float x = Mathf.Round(player.position.x * 2) / 2;
        float y = Mathf.Round(player.position.y * 2) / 2;
        return new Vector3(x, y, -1);
    }





}

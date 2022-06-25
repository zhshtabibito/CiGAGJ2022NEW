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




    // Start is called before the first frame update
    void Start()
    {
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
                    if (Mathf.Abs(m.transform.position.x - player.position.x) < 0.5f ||
                        Mathf.Abs(m.transform.position.y - player.position.y) < 0.5f)
                    {
                        MirrorList.Remove(m);
                        MirrorLeft++;
                        Object.Destroy(m);
                        return;
                    }
                }
            }
            if (MirrorLeft > 0 && player.GetComponent<Player>().CheckWall())
            {
                SetMirror();
            }
        }
    }

    private void SetNum()
    {
        numObj.GetComponent<TMP_Text>().text = $"X{MirrorLeft}";
    }

    private void SetMirror()
    {
        GameObject m = Object.Instantiate((GameObject)Resources.Load("Prefabs/Mirror/Mirror"));
        m.transform.position = AllignPos();
        MirrorList.Add(m);
    }

    private Vector3 AllignPos()
    {
        float x = Mathf.Round(player.position.x * 2) / 2;
        float y = Mathf.Round(player.position.y * 2) / 2;
        return new Vector3(x, y, 0);
    }





}

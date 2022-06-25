using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelPanel : BasePanel
{
    static readonly string Path = "Prefabs/Panels/LevelPanel";
    private Transform numObj;
    private LevelInfo info;

    private Transform player;
    private int mirrorMax;
    private int mirrorLeft;
    private List<GameObject> mirrorList;

    public LevelPanel() : base(new PanelInfo(Path))
    {

    }


    protected override void InitEvent()
    {
        ActivePanel.GetOrAddComponentInChildren<Button>("BtnMirror").onClick.AddListener(() =>
        {
            OnBtnMirror();
        });
    }

    private void OnBtnMirror()
    {
        if (mirrorLeft < mirrorMax)
        {
            foreach (GameObject m in mirrorList)
            {
                if (Mathf.Abs(m.transform.position.x - player.position.x) < 0.5f ||
                    Mathf.Abs(m.transform.position.y - player.position.y) < 0.5f)
                {
                    mirrorList.Remove(m);
                    mirrorLeft++;
                    Object.Destroy(m);
                    return;
                }
            }
        }
        if(mirrorLeft > 0 && player.GetComponent<Player>().CheckWall())
        {
            SetMirror();
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
        info = GameObject.Find("LevelRoot").GetComponent<LevelInfo>();
        mirrorList = new List<GameObject>();
        mirrorMax = info.MirrorNum;
        mirrorLeft = mirrorMax;
        numObj = GameObject.Find("TextMirrorNum").transform;
        numObj.GetComponent<TMP_Text>().text = $"X{mirrorLeft}";

        player = Player.Instance.transform;
    }

    private void SetMirror()
    {
        GameObject m = Object.Instantiate((GameObject)Resources.Load("Prefabs/Mirror/Mirror"));
        m.transform.position = AllignPos();
        mirrorList.Add(m);
    }

    private Vector3 AllignPos() 
    {
        float x = Mathf.Round(player.position.x * 2) / 2;
        float y = Mathf.Round(player.position.y * 2) / 2;
        return new Vector3(x, y, 0);
    }
}


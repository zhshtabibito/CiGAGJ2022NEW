using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPanel : BasePanel
{
    static readonly string Path = "Prefabs/Panels/LevelPanel";
    private Transform numObj;
    private LevelInfo info;
    private int mirrorLeft;

    public LevelPanel() : base(new PanelInfo(Path))
    {

    }


    protected override void InitEvent()
    {
        ActivePanel.GetOrAddComponentInChildren<Button>("ButtonMirror").onClick.AddListener(() =>
        {
            OnBtnMirror();
        });
    }

    private void OnBtnMirror()
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();
        info = GameObject.Find("LevelRoot").GetComponent<LevelInfo>();
        mirrorLeft = info.MirrorNum;
        numObj = ActivePanel.Find("ButtonMirror").Find("TextMirrorNum");
        numObj.GetComponent<TextMesh>().text = $"X{mirrorLeft}";
    }
}


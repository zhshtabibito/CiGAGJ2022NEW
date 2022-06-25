using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelPanel : BasePanel
{
    static readonly string Path = "Prefabs/Panels/LevelPanel";

    public LevelPanel() : base(new PanelInfo(Path))
    {

    }


    protected override void InitEvent()
    {
        //ActivePanel.GetOrAddComponentInChildren<Button>("BtnMirror").onClick.AddListener(() =>
        //{
        //    OnBtnMirror();
        //});
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }


}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartPanel : BasePanel
{
    static readonly string Path = "Prefabs/Panels/StartPanel";
    private Transform Chapters;

    public StartPanel() : base(new PanelInfo(Path))
    {

    }

    protected override void InitEvent()
    {
        ActivePanel.GetOrAddComponentInChildren<Button>("ButtonStart").onClick.AddListener(() =>
        {
            OnBtnStart();
        });
    }

    private void OnBtnStart()
    {
        if (SavingManager.Level > 0)
        {
            Chapters.gameObject.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("Level1");
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Chapters = ActivePanel.Find("Chapters");
    }

}

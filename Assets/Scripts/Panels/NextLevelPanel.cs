using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextLevelPanel : BasePanel
{
    static readonly string Path = "Prefabs/Panels/NextLevelPanel";

    public NextLevelPanel() : base(new PanelInfo(Path))
    {

    }

    protected override void InitEvent()
    {
        Debug.Log(ActivePanel.GetOrAddComponentInChildren<Button>("Button"));
        ActivePanel.GetOrAddComponentInChildren<Button>("Button").onClick.AddListener(() =>
        {
            OnNext();
        });
    }

    private void OnNext()
    {
        int nextID = GameObject.Find("LevelRoot").GetComponent<LevelInfo>().LevelID + 1;
        if (nextID > 7)
        {
            SceneManager.LoadScene("Start");
            PanelManager.Instance.Pop();
            PanelManager.Instance.Push(new StartPanel());
        }
        else
        {
            SceneManager.LoadScene($"Level{nextID}");
            PanelManager.Instance.Pop();
            PanelManager.Instance.Push(new LevelPanel());
        }
    }



}

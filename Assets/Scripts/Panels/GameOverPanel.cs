using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOverPanel : BasePanel
{
    static readonly string Path = "Prefabs/Panels/GameOverPanel";
    public GameOverPanel() : base(new PanelInfo(Path))
    {

    }

    protected override void InitEvent()
    {
        ActivePanel.GetOrAddComponentInChildren<Button>("Button").onClick.AddListener(() =>
        {
            OnRetry();
        });
    }
    private void OnRetry()
    {
        SceneManager.LoadScene($"Level{GameObject.Find("LevelRoot").GetComponent<LevelInfo>().LevelID}");
        PanelManager.Instance.Pop();
        PanelManager.Instance.Push(new LevelPanel());
    }

}

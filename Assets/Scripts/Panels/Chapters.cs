using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Chapters : MonoBehaviour
{
    public void LoadSceneByNum(int n)
    {
        SceneManager.LoadScene($"Level{n}");
        PanelManager.Instance.Pop();
        PanelManager.Instance.Push(new LevelPanel());
    }
}

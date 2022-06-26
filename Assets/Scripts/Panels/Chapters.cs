using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Chapters : MonoBehaviour
{
    public List<GameObject> btns;
    public List<GameObject> locks;

    public void LoadSceneByNum(int n)
    {
        SceneManager.LoadScene($"Level{n}");
        PanelManager.Instance.Pop();
        PanelManager.Instance.Push(new LevelPanel());
    }

    void Update()
    {
        int SL = Mathf.Min(SavingManager.Level, 6);
        for (int i = 0; i < SL; i++)
        {
            btns[i].GetComponent<Button>().interactable = true;
            locks[i].SetActive(false);
        }
        for (int i = SL; i < 7; i++)
        {
            btns[i].GetComponent<Button>().interactable = false;
            locks[i].SetActive(true);
        }

    }
}

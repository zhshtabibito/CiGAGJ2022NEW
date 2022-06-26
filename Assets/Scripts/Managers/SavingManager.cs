using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingManager : MonoBehaviour
{
    public static SavingManager Instance;
    public static int Level;

    private void Awake()
    {
        Instance = this;
        Level = LoadGame();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveGame(int CurrentLevel)
    {
        if(CurrentLevel > Level)
        {
            Level = CurrentLevel;
            PlayerPrefs.SetInt("Chapter", CurrentLevel);
        }
    }

    private int LoadGame()
    {
        // should be 0
        return PlayerPrefs.GetInt("Chapter", 0);
    }

}

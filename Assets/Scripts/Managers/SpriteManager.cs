using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public static SpriteManager Instance;
    public List<GameObject> Grounds;
    private static List<Sprite> GroundSprites;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        GroundSprites = new List<Sprite>();
        foreach (GameObject obj in Grounds)
        {
            GroundSprites.Add(obj.GetComponent<SpriteRenderer>().sprite);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Sprite RandomGround()
    {
        int r = Random.Range(0, 12);
        return GroundSprites[r];
    }

}

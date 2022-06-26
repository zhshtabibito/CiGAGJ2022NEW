using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharBase : MonoBehaviour
{
    public float spd = 1;
    protected float scale = 1;
    protected Vector3 dir;

    public Transform DetectorPrefab;
    public List<GameObject> Detectors;

    // Start is called before the first frame update
    void Start()
    {
        scale = Mathf.Abs(transform.localScale.x);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

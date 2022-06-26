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
    protected Animator animator;
    protected int WASD = 0;

    // Start is called before the first frame update
    void Start()
    {
        scale = Mathf.Abs(transform.localScale.x);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void SetDir(int n)
    {
        WASD = n;
        animator.SetInteger("WASD", WASD);
        switch (n)
        {
            case 1: // W
            case 2: // A
            case 3: // S
                dir = Vector3.one;
                break;
            default: // D
                dir = new Vector3(-1, 1, 1);
                break;
        }
    }
}

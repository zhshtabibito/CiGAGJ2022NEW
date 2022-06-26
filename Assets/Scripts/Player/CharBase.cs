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
        switch (n)
        {
            case 1: // W
                dir = Vector3.one;
                animator.SetInteger("WASD", 1);
                break;
            case 2: // A
                dir = Vector3.one;
                animator.SetInteger("WASD", 2);
                break;
            case 3: // S
                dir = Vector3.one;
                animator.SetInteger("WASD", 3);
                break;
            default: // D
                dir = new Vector3(-1, 1, 1);
                animator.SetInteger("WASD", 2);
                break;
        }

    }
}

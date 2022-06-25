using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : CharBase
{
    public List<GameObject> PatrolList;

    // Move related
    private bool isMoving = false;
    private Vector3 startPos;
    private Coroutine coroutine;

    // State
    public int state = 1;

    private int STAND = 0;
    private int PATROL = 1;
    private int ALERT = 2;
    private int CHASE = 3; 
    private int ATTACK = 4;


    // Patrol
    private int PatrolLenth;
    private int PatrolNext = 1;



    // Start is called before the first frame update
    void Start()
    {
        scale = Mathf.Abs(transform.localScale.x);
        
        startPos = transform.position;

        PatrolLenth = PatrolList.Count;

        MoveToPatrolPoint(PatrolNext);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MoveToPatrolPoint(int id)
    {
        MoveToObject(PatrolList[id].transform);
    }

    private void MoveToObject(Transform obj)
    {
        Vector2 d = obj.position - transform.position;

        if (d.x < -0.1f) // left
        {
            transform.localScale = new Vector3(-1, 1, 1) * scale;
        }
        else if (d.x > 0.1f) // right
        {
            transform.localScale = new Vector3(1, 1, 1) * scale;
        }
        else if(d.y < -0.1f) // down
        {
            transform.localScale = new Vector3(1, -1, 1) * scale;
        }
        else if (d.y > 0.1f) // up
        {
            transform.localScale = new Vector3(1, 1, 1) * scale;
        }

        // Start coroutine
        // anime state
        coroutine = StartCoroutine(MoveTo(obj.position));
    }

    private IEnumerator MoveTo(Vector3 tar)
    {
        isMoving = true;
        float t = (tar - startPos).magnitude / spd;
        for (float iterator = 0f; iterator < 1.0f; iterator += Time.deltaTime / t)
        {
            transform.position = new Vector3(
                Mathf.SmoothStep(startPos.x, tar.x, iterator),
                Mathf.SmoothStep(startPos.y, tar.y, iterator),
                Mathf.SmoothStep(startPos.z, tar.z, iterator));
            yield return null;
        }
        startPos = transform.position;
        // anime state
        isMoving = false;

        if(state == PATROL)
        {
            PatrolNext = (PatrolNext + 1) % PatrolLenth;
            MoveToPatrolPoint(PatrolNext);
        }
    }









}

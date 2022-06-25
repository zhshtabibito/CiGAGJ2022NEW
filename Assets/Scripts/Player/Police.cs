using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : CharBase
{
    public List<GameObject> PatrolList;
    public GameObject AlertMark;
    public GameObject QuestionMark;

    public float spdPatrol = 1;
    public float spdChase = 2;

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

    // Look
    public float SightLen = 1;
    private float SightAccu = 12;

    // Chase
    // public List<GameObject> ChaseList;
    // private int ChaseNext = 0;

    private GameObject target;
    private List<Transform> TargetList;


    // Start is called before the first frame update
    void Start()
    {
        dir = new Vector3(1, 0, 0);
        spd = spdPatrol;
        scale = Mathf.Abs(transform.localScale.x);

        startPos = transform.position;
        PatrolLenth = PatrolList.Count;

        TargetList = new List<Transform>();

        MoveToPatrolPoint(PatrolNext);
    }

    // Update is called once per frame
    void Update()
    {
        if (state == PATROL)
        {
            if (RayFan()) // find player
            {
                StopCoroutine(coroutine);
                startPos = transform.position;
                state = ALERT;
                StartCoroutine("WaitAndChase");
            }
        }
        else if (state == CHASE && !isMoving)
        {
            if (RayFan())
            {
                //׷target

            }
        }
    }

    private void MoveToTar()
    {


    }



    private IEnumerator WaitAndChase()
    {
        AlertMark.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        AlertMark.SetActive(false);
        state = CHASE;
        Debug.Log("start chase");
        // MoveToChasePoint(ChaseNext); // ChaseNext = 0
    }

    //private void MoveToChasePoint(int id)
    //{
    //    MoveToObject(ChaseList[id].transform);
    //}


    private void MoveToPatrolPoint(int id)
    {
        MoveToObject(PatrolList[id].transform);
    }

    private void MoveToObject(Transform obj)
    {
        Vector2 d = obj.position - transform.position;

        if (d.x < -0.1f) // left
        {
            dir = new Vector3(-1, 0, 0);
            transform.localScale = new Vector3(-1, 1, 1) * scale;
        }
        else if (d.x > 0.1f) // right
        {
            dir = new Vector3(1, 0, 0);
            transform.localScale = new Vector3(1, 1, 1) * scale;
        }
        else if(d.y < -0.1f) // down
        {
            dir = new Vector3(0, -1, 0);
            transform.localScale = new Vector3(1, -1, 1) * scale;
        }
        else if (d.y > 0.1f) // up
        {
            dir = new Vector3(0, 1, 0);
            transform.localScale = new Vector3(1, 1, 1) * scale;
        }

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
        else if(state == CHASE)
        {
            // ֱ��ray���о�׷�����list�ӵ�ǰ���λ��

            // ���·������


        }
    }

    // ���μ�� -> ��
    private bool RayFan()
    {
        // һ����ǰ������
        //if (RayLine(dir))
        //    return true;

        // ��һ����ȷ�ȾͶ������ԳƵ�����,ÿ�����߼н����ܽǶȳ��뾫��
        //float subAngle = (90f / 2) / SightAccu;
        //for (int i = 0; i < SightAccu; i++)
        //{
        //    Vector3 A1 = Quaternion.Euler(0, 0, subAngle * (i + 1)) * dir;
        //    Vector3 A2 = Quaternion.Euler(0, 0, -1 * subAngle * (i + 1)) * dir;

        //    if (RayLine(new Vector2(A1.x,A1.y)) || RayLine(new Vector2(A2.x, A2.y)))
        //        return true;
        //}

        float subAngle = 360f / SightAccu;
        for (int i = 0; i < SightAccu; i++)
        {
            Vector3 A = Quaternion.Euler(0, 0, subAngle * (i)) * dir;
            RayLine(new Vector2(A.x, A.y));
        }
        if(TargetList.Count > 0)
        {
            float d = 99999f;
            foreach(Transform t in TargetList)
            {
                float dd = (t.position - transform.position).magnitude;
                if ( dd< d)
                {
                    target = t.gameObject;
                    d = dd;
                }
            }
            TargetList.Clear();
            return true;
        }
        else
        {
            // ɶҲû����
            return false;
        }
    }

    // ������߼���Ƿ���Player
    private void RayLine(Vector2 RayDir)
    {
        Debug.DrawRay(transform.position, RayDir.normalized * SightLen, Color.yellow);
        RaycastHit2D[] hitList = Physics2D.RaycastAll(transform.position, RayDir, SightLen);
        for (int i = 0; i < hitList.Length; i++)
        {
            // Debug.Log(hitList[i].collider.gameObject.name);
            if (hitList[i].collider != null)
            {
                if (hitList[i].collider.CompareTag("Player") || hitList[i].collider.CompareTag("Shadow"))
                {
                    Debug.Log("Police find sth");
                    if (!TargetList.Contains(hitList[i].collider.transform))
                    {
                        TargetList.Add(hitList[i].collider.transform);
                    }
                }
            }
        }   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Game Over");
            Time.timeScale = 0;
            PanelManager.Instance.Push(new GameOverPanel());
        }
        else if (collision.CompareTag("Shadow"))
        {
        
        }
    }
}

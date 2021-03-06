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
    private int state = 0;

    private int STAND = 0;
    private int PATROL = 1;
    private int ALERT = 2;
    private int CHASE = 3; 
    private int ATTACK = 4;
    private int DIZZY = 5;
    private int BACK = 6;


    // Patrol
    private int PatrolLenth;
    private int PatrolNext = 1;

    // Look
    public float SightLen = 1;
    private float SightAccu = 24;

    // Chase
    // public List<GameObject> ChaseList;
    // private int ChaseNext = 0;

    private GameObject target;
    private List<Transform> TargetList;
    private List<Vector3> ChasePointList;

    public void OnPrepared()
    {
        state = 1;
        MoveToPatrolPoint(PatrolNext);
    }

    // Start is called before the first frame update
    void Start()
    {
        dir = new Vector3(1, 0, 0);
        spd = spdPatrol;
        scale = Mathf.Abs(transform.localScale.x);
        animator = GetComponent<Animator>();

        startPos = transform.position;
        PatrolLenth = PatrolList.Count;

        TargetList = new List<Transform>();
        ChasePointList = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log(state);
        }

        if (state == PATROL || state == DIZZY ||state == BACK)
        {
            if (RayFan()) // find player
            {
                StopCoroutine(coroutine);
                isMoving = false;
                startPos = transform.position;
                state = ALERT;
                StartCoroutine("WaitAndChase");
            }
            else if (state == BACK && !isMoving)
            {
                if (ChasePointList.Count == 0)
                {
                    state = PATROL;
                    MoveToPatrolPoint(PatrolNext);
                }
                else
                {
                    StartCoroutine(MoveTo(ChasePointList[ChasePointList.Count - 1]));
                }
            }
        }
        else if (state == CHASE && !isMoving)
        {
            if (RayFan())
            {
                MoveToTar();
            }
            else
            {
                StartCoroutine("DizzyAndBack");
            }
        }


        DetectorPrefab.localScale = dir;
    }

    private void MoveToTar()
    {
        Vector3 d = target.transform.position - transform.position;
        // Debug.Log($"{d},{!Detectors[0].GetComponent<Detector>().CheckWall()},{!Detectors[1].GetComponent<Detector>().CheckWall()},{!Detectors[2].GetComponent<Detector>().CheckWall()},{!Detectors[3].GetComponent<Detector>().CheckWall()}");
        List<int> ways = new List<int>();
        if (d.x < 0 && !Detectors[0].GetComponent<Detector>().CheckWall())
        {
            ways.Add(1);
        }
        if (d.y > 0 && !Detectors[1].GetComponent<Detector>().CheckWall())
        {
            ways.Add(2);
        }
        if (d.x > 0 && !Detectors[2].GetComponent<Detector>().CheckWall())
        {
            ways.Add(3);
        }
        if (d.y < 0 && !Detectors[3].GetComponent<Detector>().CheckWall())
        {
            ways.Add(4);
        }
        int p = Random.Range(0, ways.Count);

        switch (ways[p])
        {
            case 1: 
                coroutine = StartCoroutine(MoveTo(AllignPos(transform.position + new Vector3(-0.5f, 0, 0))));
                break;
            case 2:
                coroutine = StartCoroutine(MoveTo(AllignPos(transform.position + new Vector3(0, 0.5f, 0))));
                break;
            case 3:
                coroutine = StartCoroutine(MoveTo(AllignPos(transform.position + new Vector3(0.5f, 0, 0))));
                break;
            case 4:
                coroutine = StartCoroutine(MoveTo(AllignPos(transform.position + new Vector3(0, -0.5f, 0))));
                break;
            default:
                break;
        }
    }

    private IEnumerator DizzyAndBack()
    {
        state = DIZZY;
        QuestionMark.SetActive(true);
        int temp = WASD;
        yield return new WaitForSeconds(0.5f);
        SetDir((WASD + 1) == 5 ? 1 : (WASD + 1));
        // yield return new WaitForSeconds(0.5f);
        // SetDir(temp);
        yield return new WaitForSeconds(0.5f);
        SetDir((WASD - 1) == 0 ? 4 : (WASD - 1));
        // yield return new WaitForSeconds(0.5f);
        // SetDir(temp);
        QuestionMark.SetActive(false);
        state = BACK;
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

    private void MoveToPatrolPoint(int id)
    {
        MoveToObject(PatrolList[id].transform);
    }

    private void MoveToObject(Transform obj)
    {
        coroutine = StartCoroutine(MoveTo(obj.position));
    }

    private IEnumerator MoveTo(Vector3 tar)
    {
        Vector2 d = tar - transform.position;
        isMoving = true;

        if (d.x < -0.1f) // left
        {
            SetDir(2);
        }
        else if (d.x > 0.1f) // right
        {
            SetDir(4);
        }
        else if (d.y < -0.1f) // down
        {
            SetDir(3);
        }
        else if (d.y > 0.1f) // up
        {
            SetDir(1);
        }
        transform.localScale = dir * scale;

        if (state == CHASE)
        {
            spd = spdChase;
            ChasePointList.Add(new Vector3(transform.position.x, transform.position.y, transform.position.z));
        }
        else
        {
            spd = spdPatrol;
        }

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
        else if(state == BACK)
        {
            ChasePointList.RemoveAt(ChasePointList.Count-1);
        }
    }

    // ????????????????? -> ??????
    private bool RayFan()
    {
        // ??????????????????????????????????
        //if (RayLine(dir))
        //    return true;

        // ??????????????????????????????????????????????????????????????????,??????????????????????????????????????????????????????
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
            // find nothing
            target = null;
            return false;
        }
    }

    // ????????????????????????????????????????Player
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
        if (state == STAND)
            return;

        if (collision.CompareTag("Player"))
        {
            Debug.Log("Game Over");
            Time.timeScale = 0;
            PanelManager.Instance.Push(new GameOverPanel());
        }
        else if (collision.CompareTag("Shadow"))
        {
            Debug.Log("shadow");
            collision.GetComponent<Img>().isCaught = true;
            // fuck mirror and shadow here



            // ai behave
            StopCoroutine(coroutine);
            isMoving = false;
            startPos = transform.position;
            StartCoroutine("DizzyAndBack");
        }
    }

    private Vector3 AllignPos(Vector3 v)
    {
        float x = Mathf.Round(v.x * 2) / 2;
        float y = Mathf.Round(v.y * 2) / 2;
        return new Vector3(x, y, 0);
    }
}

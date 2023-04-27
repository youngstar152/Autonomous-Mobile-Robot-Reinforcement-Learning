using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using System.Numerics;
public class CollisionDetector : MonoBehaviour
{
    public Transform human;
    int rand;
    int rand2;
    int rand3;
    int randWhole;
    int arc;
    public float speed2;
    public float movedis;
    Vector3 myPos2;
    Vector3 resetPos;
    //public checkyoko check1;
    //public checkyoko check2;
    //public checkyoko check3;
    //public checkyoko check4;
    public WaypointMove agent;
    public Transform player;
    private NavMeshAgent navmesh;
    public RollerAgent agentlist;
    private bool enter;
    private bool looked;
    float searchAngle = 90f;

    private float rayDistance;
    private float rayDistanceR;
    private float rayDistanceL;
    private float rayDistance2;
    private float rayDistance2R;
    private float rayDistance2L;
    private bool lockOn;
    private int beforeint;
    private int nowint;
    private bool lockOnR;
    private int beforeintR;
    private int nowintR;
    private bool lockOnL;
    private int beforeintL;
    private int nowintL;
    void Start()
    {
        //var test = Quaternion.Euler(0, 0, 0) * Vector3.forward;

        //Debug.Log("test" + test);
        myPos2 = human.localPosition;
        resetPos = human.localPosition;
        speed2 = 0.1f;
        navmesh = agent.GetComponent<NavMeshAgent>();
        enter = true;
        looked = false;

        rayDistance = 6.5f;
        rayDistanceR = 6.5f;
        rayDistanceL = 6.5f;

        rayDistance2 = 2.5f;
        rayDistance2R = 2.5f;
        rayDistance2L = 2.5f;
        lockOn = false;
        lockOnR = false;
        lockOnL= false;
        beforeint = -1;
        beforeintR = -1;
        beforeintL= -1;
    }

    private void Update()
    {
        //90度のレイ
        var direction = transform.forward;
        Vector3 rayPosition = transform.position + new Vector3(0.0f, 0.5f, 0.0f);
        Ray ray = new Ray(rayPosition, direction);
        Debug.DrawRay(rayPosition, direction * rayDistance, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            //Debug.Log(hit.collider.gameObject.tag);
            if(hit.collider.gameObject.CompareTag("Player"))
            {
                float anglehit = GetAngle(hit.collider.transform.position, human.transform.position) * -1 + 90;
                float anglehit2 = (anglehit - hit.collider.transform.rotation.eulerAngles.y + 360) % 360;
                //Debug.Log("anglehit2:"+anglehit2);
                nowint = (int)anglehit2 / 30;
                //nowint = nowint % 12;
                //Debug.Log(nowint);
                if (nowint != beforeint && nowint>=0)
                {
                    agentlist.arclist[nowint] += 1;
                    if (lockOn)
                    {
                        if (beforeint >= 0)
                        {
                            //Debug.Log("Looked:" + beforeint);
                            agentlist.arclist[beforeint] -= 1;
                        }
                    }
                }
                beforeint = nowint;
                lockOn = true;
                
            }
        }
        else if(lockOn)
        {
            lockOn = false;
            //beforeint = beforeint % 12;
            //Debug.Log(beforeint);
            if (beforeint >= 0)
            {
                agentlist.arclist[beforeint] -= 1;
            }
            
            beforeint = -1;
        }

        //80度のレイ
        var directionR = ((transform.forward*5.6713f)+transform.right).normalized;       
        Vector3 rayPositionR = transform.position + new Vector3(0.0f, 0.5f, 0.0f);
        Ray rayR = new Ray(rayPositionR, directionR);
        Debug.DrawRay(rayPositionR, directionR * rayDistanceR, Color.blue);
        RaycastHit hitR;
        if (Physics.Raycast(rayR, out hitR, rayDistanceR))
        {
            //Debug.Log(hit.collider.gameObject.tag);
            if (hitR.collider.gameObject.CompareTag("Player"))
            {
                float anglehitR = GetAngle(hitR.collider.transform.position, human.transform.position) * -1 + 90;
                float anglehit2R = (anglehitR - hitR.collider.transform.rotation.eulerAngles.y + 360) % 360;
                //Debug.Log("anglehit2:"+anglehit2);
                nowintR = (int)anglehit2R / 30;
                //Debug.Log(nowint);
                //nowintR = nowintR % 12;
                if (nowintR != beforeintR && nowintR >= 0)
                {
                    agentlist.arclist[nowintR] += 0.5f;
                    if (lockOnR)
                    {
                        //Debug.Log("Looked:" + beforeintR);
                        if (beforeintR >= 0)
                        {
                            agentlist.arclist[beforeintR] -= 0.5f;
                        }
                    }
                }
                beforeintR = nowintR;
                lockOnR = true;

            }
        }
        else if (lockOnR)
        {
            //beforeintR = beforeintR % 12;
            //Debug.Log(beforeintR);
            if (beforeintR >= 0)
            {
                agentlist.arclist[beforeintR] -= 0.5f;
            }
            lockOnR = false;
            beforeintR = -1;
        }

        //100度のレイ
        var directionL = ((transform.forward * 5.6713f) + transform.right*-1).normalized;
        Vector3 rayPositionL = transform.position + new Vector3(0.0f, 0.5f, 0.0f);
        Ray rayL = new Ray(rayPositionL, directionL);
        Debug.DrawRay(rayPositionL, directionL * rayDistanceL, Color.blue);
        RaycastHit hitL;
        if (Physics.Raycast(rayL, out hitL, rayDistanceL))
        {
            //Debug.Log(hit.collider.gameObject.tag);
            if (hitL.collider.gameObject.CompareTag("Player"))
            {
                float anglehitL = GetAngle(hitL.collider.transform.position, human.transform.position) * -1 + 90;
                float anglehit2L = (anglehitL - hitL.collider.transform.rotation.eulerAngles.y + 360) % 360;
                //Debug.Log("anglehit2:"+anglehit2);
                nowintL = (int)anglehit2L / 30;
                //nowintL = nowintL % 12;
                //Debug.Log(nowint);
                if (nowintL != beforeintL && nowintL >= 0)
                {
                    agentlist.arclist[nowintL] += 0.5f;
                    if (lockOnL)
                    {
                        if (beforeintL >= 0)
                        {
                            //Debug.Log("Looked:" + beforeintL);
                            agentlist.arclist[beforeintL] -= 0.5f;
                        }
                    }
                }
                beforeintL = nowintL;
                lockOnL = true;

            }
        }
        else if (lockOnL)
        {
            //beforeintL = beforeintL % 12;
            //Debug.Log(beforeintL);
            if (beforeintL >= 0)
            {
                agentlist.arclist[beforeintL] -= 0.5f;
            }
            lockOnL = false;
            beforeintL = -1;
        }

        //ストップの動作
        //90度のレイ
        var direction2 = transform.forward;
        Vector3 rayPosition2 = transform.position + new Vector3(0.0f, 0.5f, 0.0f);
        Ray ray2 = new Ray(rayPosition2, direction2);
        Debug.DrawRay(rayPosition2, direction2 * rayDistance2, Color.green);
        RaycastHit hit2;
        if (Physics.Raycast(ray2, out hit2, rayDistance2))
        {
            //Debug.Log(hit.collider.gameObject.tag);
            if (hit2.collider.gameObject.CompareTag("Human"))
            {
                navmesh.isStopped = true;
                StartCoroutine("HumanStop");
            }
        }

        //80度のレイ
        var direction2R = ((transform.forward * 5.6713f) + transform.right).normalized;
        Vector3 rayPosition2R = transform.position + new Vector3(0.0f, 0.5f, 0.0f);
        Ray ray2R = new Ray(rayPosition2R, direction2R);
        Debug.DrawRay(rayPosition2R, direction2R * rayDistance2R, Color.green);
        RaycastHit hit2R;
        if (Physics.Raycast(ray2R, out hit2R, rayDistance2R))
        {
            //Debug.Log(hit.collider.gameObject.tag);
            if (hit2R.collider.gameObject.CompareTag("Human"))
            {
                navmesh.isStopped = true;
                StartCoroutine("HumanStop");
            }
        }

        //100度のレイ
        var direction2L = ((transform.forward * 5.6713f) + transform.right * -1).normalized;
        Vector3 rayPosition2L = transform.position + new Vector3(0.0f, 0.5f, 0.0f);
        Ray ray2L = new Ray(rayPosition2L, direction2L);
        Debug.DrawRay(rayPosition2L, direction2L * rayDistance2L, Color.green);
        RaycastHit hit2L;
        if (Physics.Raycast(ray2L, out hit2L, rayDistance2L))
        {
            //Debug.Log(hit.collider.gameObject.tag);
            if (hit2L.collider.gameObject.CompareTag("Human"))
            {
                navmesh.isStopped = true;
                StartCoroutine("HumanStop");

            }
        }

    }


    float GetAngle(Vector3 start, Vector3 target)
    {
        Vector3 dt = target - start;
        float rad = Mathf.Atan2(dt.z, dt.x);
        float degree = rad * Mathf.Rad2Deg;

        return degree;
    }

    // 指定された角度（ 0 ～ 360 ）をベクトルに変換して返す
    public static Vector3 GetDirection(float angle)
    {
        return new Vector3
        (
            Mathf.Cos(angle * Mathf.Deg2Rad),
            0,
            Mathf.Sin(angle * Mathf.Deg2Rad)
                    );
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && enter)
        {
            //var positionDiff = collider.transform.position - human.transform.position;  // 自身（敵）とプレイヤーの距離
            //var angle = Vector3.Angle(transform.forward, positionDiff);
            //var angle1 = Vector3.Angle(human.transform.rotation, positionDiff);



            float angle2 = GetAngle(human.transform.position, collider.transform.position) * -1 + 90;
            float angle20 = (angle2 - human.transform.rotation.eulerAngles.y + 360) % 360;

            // 敵から見たプレイヤーの方向
            if (angle20 < 90)
            {
                rand = 2;
                looked = true;
                //Debug.Log("right");
            }
            if (270 < angle20 && angle20 <= 360)
            {
                rand = 1;
                looked = true;
                //Debug.Log("left");
            }

            //navmesh.isStopped=true;
            enter = false;
            //Debug.Log("c");
            rand3 = Random.Range(0, 2);
            //rand3 = 1;
            if (looked)
            {
                rand3 = Random.Range(0, 2);
                if (rand3 == 1)
                {
                    randWhole = Random.Range(1, 7);
                    //randWhole = 1;
                    if (randWhole == 1 || randWhole == 4)
                    {
                        navmesh.isStopped = true;
                        //rand = Random.Range(1, 3);
                        StartCoroutine("MoveLR");

                        //human.position = Vector3.MoveTowards(human.position, myPos2, speed2);
                    }
                    if (randWhole == 2)
                    {
                        //Debug.Log("stop");
                        //navmesh.isStopped = true;
                        //StartCoroutine("MoveBack");
                        navmesh.isStopped = true;
                        StartCoroutine("Stop");
                    }
                    if (randWhole == 3 || randWhole == 5)
                    {
                        //Debug.Log("MoveArc");
                        navmesh.isStopped = true;
                        //rand2 = Random.Range(0, 2);
                        if (rand == 1)
                        {
                            arc = Random.Range(46, 90);
                        }
                        else
                        {
                            arc = Random.Range(-90, -46);
                        }
                        StartCoroutine("MoveForward");
                    }
                    if (randWhole == 6)
                    {
                        //Debug.Log("through");
                        enter = true;
                    }
                }
                else
                {
                    //Debug.Log("through");
                    enter = true;
                    looked = false;
                }

            }
            else
            {
                //Debug.Log("through");
                enter = true;
                looked = false;
            }

        }
    }

    IEnumerator HumanStop()
    {
        yield return new WaitForSeconds(1.75f);
        navmesh.isStopped = false;
    }
    IEnumerator Stop()
    {
        Quaternion moveRotation = Quaternion.LookRotation(player.transform.position - human.localPosition, Vector3.up);
        moveRotation.z = 0;
        moveRotation.x = 0;
        var rou = human.rotation;
        //transform.rotation = Quaternion.Lerp(transform.rotation, moveRotation, 0.1f);
        Quaternion a = Quaternion.Lerp(human.transform.rotation, moveRotation, 0.1f);
        Quaternion b = Quaternion.AngleAxis(180, Vector3.up);
        human.transform.rotation = a * b;

        //Vector3 course = new Vector3(0, arc, 0);
        //human.localRotation = Quaternion.Euler(course);
        human.localPosition += human.forward * 0.1f;
        yield return new WaitForSeconds(0.3f);
        human.transform.rotation = rou;
        yield return new WaitForSeconds(8.0f);
        navmesh.isStopped = false;
        //StartCoroutine("Stop");
        enter = true;
        looked = false;
        //Debug.Log("restart");
    }
    IEnumerator MoveLR()
    {
        if (rand == 1)
        {
            var rou = human.rotation;
            Quaternion moveRotation = Quaternion.LookRotation(player.transform.position - human.localPosition, Vector3.up);
            moveRotation.z = 0;
            moveRotation.x = 0;
            //transform.rotation = Quaternion.Lerp(transform.rotation, moveRotation, 0.1f);
            Quaternion a = Quaternion.Lerp(human.transform.rotation, moveRotation, 0.1f);
            Quaternion b = Quaternion.AngleAxis(90, Vector3.up);
            human.transform.rotation = a * b;

            //Vector3 rightturn = new Vector3(0, 0, 0);
            //human.localRotation = Quaternion.Euler(rightturn);
            for (int i = 0; i < 15; i++)
            {
                human.position += human.forward * 0.1f;
                yield return new WaitForSeconds(0.3f);
            }
            human.transform.rotation = rou;
            yield return new WaitForSeconds(4.0f);
        }
        else
        {
            //Debug.Log("leftturn");

            Quaternion moveRotation = Quaternion.LookRotation(player.transform.position - human.localPosition, Vector3.up);
            moveRotation.z = 0;
            moveRotation.x = 0;
            var rou = human.rotation;
            //transform.rotation = Quaternion.Lerp(transform.rotation, moveRotation, 0.1f);
            Quaternion a = Quaternion.Lerp(human.transform.rotation, moveRotation, 0.1f);
            Quaternion b = Quaternion.AngleAxis(-90, Vector3.up);
            human.transform.rotation = a * b;

            for (int i = 0; i < 15; i++)
            {
                human.localPosition += human.forward * 0.1f;
                yield return new WaitForSeconds(0.3f);
            }
            human.rotation = rou;
            yield return new WaitForSeconds(4.0f);
        }
        navmesh.isStopped = false;
        //StartCoroutine("Stop");
        enter = true;
        looked = false;
        //Debug.Log("restart");
        //myPos2 = resetPos;

    }
    IEnumerator MoveBack()
    {
        Quaternion moveRotation = Quaternion.LookRotation(player.transform.position - human.localPosition, Vector3.up);
        moveRotation.z = 0;
        moveRotation.x = 0;
        var rou = human.rotation;
        //transform.rotation = Quaternion.Lerp(transform.rotation, moveRotation, 0.1f);
        Quaternion a = Quaternion.Lerp(human.transform.rotation, moveRotation, 0.1f);
        Quaternion b = Quaternion.AngleAxis(180, Vector3.up);
        human.transform.rotation = a * b;

        //Vector3 backturn = new Vector3(0, 270, 0);
        //human.localRotation = Quaternion.Euler(backturn);
        for (int i = 0; i < 25; i++)
        {
            human.localPosition += human.forward * 0.1f;
            yield return new WaitForSeconds(0.3f);
        }
        human.rotation = rou;
        yield return new WaitForSeconds(4.0f);

        navmesh.isStopped = false;
        //StartCoroutine("Stop");
        enter = true;
        looked = false;
    }

    IEnumerator MoveForward()
    {
        //Debug.Log("arcRandom");
        Quaternion moveRotation = Quaternion.LookRotation(player.transform.position - human.localPosition, Vector3.up);
        moveRotation.z = 0;
        moveRotation.x = 0;
        var rou = human.rotation;
        //transform.rotation = Quaternion.Lerp(transform.rotation, moveRotation, 0.1f);
        Quaternion a = Quaternion.Lerp(human.transform.rotation, moveRotation, 0.1f);
        Quaternion b = Quaternion.AngleAxis(arc, Vector3.up);
        human.transform.rotation = a * b;

        //Vector3 course = new Vector3(0, arc, 0);
        //human.localRotation = Quaternion.Euler(course);
        for (int i = 0; i < 15; i++)
        {
            human.localPosition += human.forward * 0.1f;
            yield return new WaitForSeconds(0.3f);
        }
        human.transform.rotation = rou;
        yield return new WaitForSeconds(4.0f);

        navmesh.isStopped = false;
        //StartCoroutine("Stop");
        enter = true;
        looked = false;
    }
}

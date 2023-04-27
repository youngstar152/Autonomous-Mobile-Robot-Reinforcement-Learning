//using System.Collections.Generic;
//using UnityEngine;
//using Unity.MLAgents;
//using Unity.MLAgents.Sensors;
//using Unity.MLAgents.Actuators;
//using Unity.MLAgents.Policies;
//using System;

//// RollerAgent
//public class RollerAgent : Agent
//{
//    public Transform target; // TargetのTransform
//    Rigidbody rBody; // RollerAgentのRigidBody

//    // 初期化時に呼ばれる
//    public override void Initialize()
//    {
//        // RollerAgentのRigidBodyの参照の取得
//        this.rBody = GetComponent<Rigidbody>();
//    }

//    // エピソード開始時に呼ばれる
//    public override void OnEpisodeBegin()
//    {
//        // RollerAgentが床から落下している時
//        if (this.transform.localPosition.y < 0)
//        {
//            // RollerAgentの位置と速度をリセット
//            this.rBody.angularVelocity = Vector3.zero;
//            this.rBody.velocity = Vector3.zero;
//            this.transform.localPosition = new Vector3(0.0f, 0.5f, 0.0f);
//        }

//        // Targetの位置のリセット
//        target.localPosition = new Vector3(
//            UnityEngine.Random.value*8-4, 0.5f, UnityEngine.Random.value*8 - 4);
//    }

//    // 状態取得時に呼ばれる
//    //public override void CollectObservations(VectorSensor sensor)
//    //{
//    //    sensor.AddObservation(target.localPosition.x); //TargetのX座標
//    //    sensor.AddObservation(target.localPosition.z); //TargetのZ座標
//    //    sensor.AddObservation(this.transform.localPosition.x); //RollerAgentのX座標
//    //    sensor.AddObservation(this.transform.localPosition.z); //RollerAgentのZ座標
//    //    sensor.AddObservation(rBody.velocity.x); // RollerAgentのX速度
//    //    sensor.AddObservation(rBody.velocity.z); // RollerAgentのZ速度
//    //}

//    // 行動実行時に呼ばれる
//    public override void OnActionReceived(ActionBuffers actionBuffers)
//    {
//        // RollerAgentに力を加える
//        Vector3 controlSignal = Vector3.zero;
//        controlSignal.x = actionBuffers.ContinuousActions[0];
//        controlSignal.z = actionBuffers.ContinuousActions[1];
//        rBody.AddForce(controlSignal * 10);

//        // RollerAgentがTargetの位置にたどりついた時
//        float distanceToTarget = Vector3.Distance(
//            this.transform.localPosition, target.localPosition);
//        if (distanceToTarget < 1.42f)
//        {
//            AddReward(1.0f);
//            EndEpisode();
//        }

//        // RollerAgentが床から落下した時
//        if (this.transform.localPosition.y < 0)
//        {
//            EndEpisode();
//        }
//    }

//    // ヒューリスティックモードの行動決定時に呼ばれる
//    public override void Heuristic(in ActionBuffers actionsOut)
//    {
//        var continuousActionsOut = actionsOut.ContinuousActions;
//        continuousActionsOut[0] = Input.GetAxis("Horizontal");
//        continuousActionsOut[1] = Input.GetAxis("Vertical");
//    }
//}



//Transform myTransform = this.transform;
//Vector3 myPos = myTransform.position;
//Vector3 desPos;
//myPos.y = 0.3f;
//Navigation
//+nav
//if (navMeshAgent && navMeshAgent.enabled)
//{
//    navMeshAgent.SetDestination(this.transform.position);
//    navMeshAgent.SetDestination(target.position);
//    desPos = target.position;
//    // 経路取得用のインスタンス作成
//    //path = new NavMeshPath();
//    //// 明示的な経路計算実行
//    //navMeshAgent.CalculatePath(target.position, path);

//    //// LineRendererで経路描画！
//    //line.SetVertexCount(path.corners.Length);
//    //line.SetPositions(path.corners);

//    foreach (var pos in navMeshAgent.path.corners)
//    {
//        var diff2d = new Vector2(
//            Mathf.Abs(pos.x - transform.position.x),
//            Mathf.Abs(pos.z - transform.position.z)
//        );

//        if (0.1f <= diff2d.magnitude)
//        {
//            //target.position = pos;
//            desPos = pos;
//            break;
//        }
//    }
//    myPos.x = transform.position.x;
//    myPos.z = transform.position.z;
//    gameObject.transform.position = myPos;

//    //Debug.DrawLine(transform.position, target.position, Color.red);
//    //Debug.DrawLine(myPos, desPos, Color.red);
//    //Debug.Log(transform.position);

//    //if (navMeshAgent && navMeshAgent.enabled)
//    //{
//    //    Gizmos.color = Color.red;
//    //    var prefPos = transform.position;

//    //    foreach (var pos in navMeshAgent.path.corners)
//    //    {
//    //        Gizmos.DrawLine(prefPos, pos);
//    //        prefPos = pos;
//    //    }
//    //}

//    //Quaternion moveRotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
//    Quaternion moveRotation = Quaternion.LookRotation(desPos - myPos, Vector3.up);
//    moveRotation.z = 0;
//    moveRotation.x = 0;
//transform.rotation = Quaternion.Lerp(transform.rotation, moveRotation, 0.1f);
//Debug.Log("move" + moveRotation.y);

//humanPos.position = humanReset;
//Vector3 course2 = new Vector3(0, 90, 0);
//humanPos.rotation = Quaternion.Euler(course2);
//check1.gameObject.SetActive(false);
//check2.gameObject.SetActive(false);
//check3.gameObject.SetActive(false);
//check4.gameObject.SetActive(false);
//once = 1;
//this.transform.localPosition = new Vector3(1.1f, 0.5f, 7.93f);
//this.transform.localPosition = new Vector3(5.92f, 0.5f, 7.93f);
//}

// Targetの位置のリセット
//target.localPosition = new Vector3(
//    UnityEngine.Random.value * 8 - 4, 0.5f, UnityEngine.Random.value * 8 - 4);


// 経路取得用のインスタンス作成
//path = new NavMeshPath();
//// 明示的な経路計算実行
//navMeshAgent.CalculatePath(target.position, path);

//// LineRendererで経路描画！
//line.SetVertexCount(path.corners.Length);
//line.SetPositions(path.corners);

//Debug.DrawLine(transform.position, target.position, Color.red);
//Debug.DrawLine(myPos, desPos, Color.red);
//Debug.Log(transform.position);

//if (navMeshAgent && navMeshAgent.enabled)
//{
//    Gizmos.color = Color.red;
//    var prefPos = transform.position;

//    foreach (var pos in navMeshAgent.path.corners)
//    {
//        Gizmos.DrawLine(prefPos, pos);
//        prefPos = pos;
//    }
//}

using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine.AI;
using System.IO;
using System.Text;
using System.Linq;

//+nav
[RequireComponent(typeof(NavMeshAgent))]
// RollerAgent
public class RollerAgent : Agent
{
    public Transform target; // TargetのTransform
    Rigidbody rBody; // RollerAgentのRigidBody
    //public Transform humanPos;
    //private Vector3 humanReset;
    private Vector3 agentReset;
    private bool position;
    //public checkyoko check1;
    //public checkyoko check2;
    //public checkyoko check3;
    //public checkyoko check4;
    private int once;
    NavMeshPath path;
    LineRenderer line;
    public WaypointMove[] players;
    public Transform[] waypoints;

    public Transform[] startPointsUp;
    public Transform[] startPointsDown;
    //public Transform[] starttest;
    private int UpDn;
    private int agentInt;
    private int targetInt;
    //private int startInt;

    private Vector3[] players_position;
    private NavMeshAgent[] navmeshs;
    private int rot;

    //public WaypointMove player_pos1;
    //private Vector3 Vplayer_pos1;
    //public WaypointMove player_pos2;
    //private Vector3 Vplayer_pos2;

    //+nav
    protected NavMeshAgent navMeshAgent;
    public float moveSpeed;
    private NavMeshAgent navmesh1;
    private NavMeshAgent navmesh2;

    Transform myTransform;
    Vector3 myPos;
    Vector3 desPos;
    Quaternion moveRotation;
    Quaternion beforeArcb;

    public float[] arclist;
    private StreamWriter sw;
    private int swstep;
    private int sweps;
    private float swscore;
    private string swresult;
    private float ypos;

    private void Start()
    {
        //FileStream fs = File.OpenWrite("Assets/Logs/SaveData.csv");
        //sw = new StreamWriter("Assets/Logs/SaveData.csv", true, Encoding.GetEncoding("Shift_JIS"));
        //sw = new StreamWriter("SaveData.txt", false, Encoding.GetEncoding("Shift_JIS"));
        swstep =0;
        sweps=0;
        swscore = 0f;
        swresult = "start";
        ypos = 1.0f;

        //humanReset = humanPos.position;
        agentReset = this.transform.position;
        //Vplayer_pos1 = player_pos1.transform.position;
        //Vplayer_pos2 = player_pos2.transform.position;
        //navmesh1 = player_pos1.GetComponent<NavMeshAgent>();
        //navmesh2 = player_pos1.GetComponent<NavMeshAgent>();
        arclist = new float[12]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0 };
        position = true;
        players_position = new Vector3[players.Length];
        navmeshs = new NavMeshAgent[players.Length];
        rot = 0;
        beforeArcb = Quaternion.Euler(0, 0, 0);
        for (int i = 0; i < players.Length; i++)
        {
            players_position[i] = players[i].transform.position;
            navmeshs[i]=players[i].GetComponent<NavMeshAgent>();
        }
        this.rBody = GetComponent<Rigidbody>();
    }

    // 初期化時に呼ばれる
    public override void Initialize()
    {
        // RollerAgentのRigidBodyの参照の取得
        this.rBody = GetComponent<Rigidbody>();
        //+nav
        navMeshAgent = this.GetComponent<NavMeshAgent>();
    }

    // エピソード開始時に呼ばれる
    public override void OnEpisodeBegin()
    {
        sw = new StreamWriter("Assets/Logs/camera-240135-testkankyo1.csv", true, Encoding.GetEncoding("Shift_JIS"));
        string memo = sweps.ToString()+":"+ swstep.ToString() + ":" + swscore.ToString() + ":" + swresult;
        sw.WriteLine(memo);
        //sw.Flush();
        sw.Close();
        Debug.Log(memo);
        swstep = 0;
        sweps += 1;
        swscore = 0f;
        // RollerAgentが床から落下している時
        //if (this.transform.localPosition.y < 0)
        //{
        // RollerAgentの位置と速度をリセット
        this.rBody.angularVelocity = Vector3.zero;
        this.rBody.velocity = Vector3.zero;
        //this.transform.position = new Vector3(100.2f, 0.35f, 127.1f);
        rot = 0;

        UpDn = Random.Range(0, 2);
        //UpDn = 0;
        agentInt = Random.Range(0, startPointsDown.Length);
        targetInt = Random.Range(0, startPointsUp.Length);
        //agentInt = 0;
        //targetInt = 0;
        //startInt = Random.Range(0, 2);
        //this.transform.position = starttest[startInt].position;
        //agentReset = this.transform.position;
        //if (startInt == 0)
        //{
        //    target.position = startPointsUp[targetInt].position;
        //}
        //else
        //{
        //    target.position = startPointsDown[agentInt].position;
        //}

        if (UpDn == 0)
        {
            this.transform.position = startPointsDown[agentInt].position;
            agentReset = this.transform.position;
            target.position = startPointsUp[targetInt].position;
            //target.position.y = ypos;
        }
        else
        {
            this.transform.position = startPointsUp[agentInt].position;
            agentReset = this.transform.position;
            target.position = startPointsDown[targetInt].position;
        }
        this.transform.position = agentReset;

        //navmesh1.isStopped = true;
        //navmesh2.isStopped = true;
        //player_pos1.transform.position=Vplayer_pos1;
        //player_pos2.transform.position = Vplayer_pos2;
        //navmesh1.isStopped = false;
        //navmesh2.isStopped = false;

        for (int i = 0; i < players.Length; i++)
        {
            navmeshs[i].isStopped = true;
            players[i].transform.position= players_position[i];
            var ind = Random.Range(0, waypoints.Length);
            navmeshs[i].SetDestination(waypoints[ind].position);
            navmeshs[i].isStopped = false;
        }
        //エージェントの初期位置
        //Debug.Log(agentReset);
        //Debug.Log(this.transform.position);
        //Debug.Log(this.transform.localPosition);
        position = true;
        beforeArcb.y = 0;
        moveRotation.y = 0;
        //this.transform.position = agentReset;
    }

    ////状態取得時に呼ばれる
    public override void CollectObservations(VectorSensor sensor)
    {
        Transform myTransform = this.transform;
        Vector3 myPos = myTransform.position;
        Vector3 desPos;
        myPos.y = 0.3f;
        moveRotation.y = 0.0f;
        //sensor.AddObservation(target.localPosition.x); //TargetのX座標
        //sensor.AddObservation(target.localPosition.z); //TargetのZ座標
        //sensor.AddObservation(this.transform.localPosition.x); //RollerAgentのX座標
        //sensor.AddObservation(this.transform.localPosition.z); //RollerAgentのZ座標
        if (navMeshAgent && navMeshAgent.enabled)
        {
            navMeshAgent.SetDestination(this.transform.position);
            navMeshAgent.SetDestination(target.position);
            desPos = target.position;

            foreach (var pos in navMeshAgent.path.corners)
            {
                var diff2d = new Vector2(
                    Mathf.Abs(pos.x - transform.position.x),
                    Mathf.Abs(pos.z - transform.position.z)
                );

                if (0.1f <= diff2d.magnitude)
                {
                    //target.position = pos;
                    desPos = pos;
                    break;
                }
            }
            myPos.x = transform.position.x;
            myPos.z = transform.position.z;
            gameObject.transform.position = myPos;

            //Quaternion moveRotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
            moveRotation = Quaternion.LookRotation(desPos - myPos, Vector3.up);
            moveRotation.z = 0;
            moveRotation.x = 0;
        }
        //Debug.Log(string.Join(",", arclist.Select(n => n.ToString())));
        sensor.AddObservation(rBody.velocity.x); // RollerAgentのX速度
        sensor.AddObservation(rBody.velocity.z); // RollerAgentのZ速度
        sensor.AddObservation(this.transform.rotation.y);
        sensor.AddObservation(moveRotation.y);
        sensor.AddObservation(arclist);
        swstep += 1;
        if (m_StepCount >= MaxStep-1)
        {
            swresult = "Miss";
            //AddReward(-2.0f);
        }
        //sensor.AddObservation(beforeArcb.y);
        //Debug.Log("sensor" + moveRotation.y);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Human")
        {
            Debug.Log("Miss");
            AddReward(-4.0f);
            swscore -= 4.0f;
            swresult = "Miss";
            EndEpisode();
        }       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Checkpoint")
        {
            //Debug.Log("checkpoint");
            AddReward(0.5f);
            swscore += 0.5f;
            //once = 2;
        }
        if (other.gameObject.tag == "Checkout")
        {
            //Debug.Log("danger");
            AddReward(-0.2f);
            swscore -= 0.2f;
        }
    }

    // 行動実行時に呼ばれる
    public override void OnActionReceived(ActionBuffers vectorActions)
    {
        //Debug.Log("action");
        //Debug.Log(this.transform.position);
        //Debug.Log(this.transform.localPosition);
        if (position)
        {
            this.transform.position = agentReset;
            position = false;
        }
        // RollerAgentに力を加える
        Vector3 dirToGo = Vector3.zero;
        Vector3 rotateDir = Vector3.zero;
        int action1 = (int)vectorActions.DiscreteActions[0];
        int action2 = (int)vectorActions.DiscreteActions[1];

        myPos.y = 0.3f;
        myPos.x = transform.position.x;
        myPos.z = transform.position.z;
        gameObject.transform.position = myPos;
        Quaternion a = Quaternion.Lerp(transform.rotation, moveRotation, 0.1f);

            float forward_x = transform.forward.x * moveSpeed;
            float forward_z = transform.forward.z * moveSpeed;

            rBody.velocity = new Vector3(forward_x, rBody.velocity.y, forward_z);
            //Navigation終わり
            
            //Ml-Agentsの出力
            if (action1 == 1) {
                dirToGo = transform.forward * moveSpeed * -1.0f;
                AddReward(-0.0004f);
                swscore -= 0.0004f;
            }
            if (action1 == 2) { 
                dirToGo = transform.forward * moveSpeed * -2.0f;
                AddReward(-0.0008f);
                swscore -= 0.0008f;
            }
            if (action2 == 1) { 
                rotateDir = transform.up * -1.0f;
                rot += 1;
            }
            if (action2 == 2) { 
                rotateDir = transform.up*-1.0f;
                rot -= 1;
            }
            if (action2 == 0)
            {
                if (rot > 0)
                {
                    rot-=1;
                }
                if (rot > 30)
                {
                    rot = 30;
                }
                if(rot<0)
                {
                    rot += 1;
                }
                if (rot < -30)
                {
                    rot = -30;
                }
            }
        if (moveRotation.y > 900)
        {
            moveRotation.y = 900;
        }
        if (moveRotation.y < -900)
        {
            moveRotation.y = -900;
        }
        if (beforeArcb.y > 900)
        {
            beforeArcb.y = 900;
        }
        if (beforeArcb.y < -900)
        {
            beforeArcb.y = -900;
        }

        //transform.Rotate(rotateDir, Time.deltaTime * 200f);
        //+nav
        Quaternion b = Quaternion.AngleAxis(Time.deltaTime * 3.0f * rot, rotateDir);
        beforeArcb = b;
        //速さの和
        //+nav
        Vector3 new_velocity = new Vector3(forward_x, rBody.velocity.y, forward_z) + (dirToGo);
        //rBody.AddForce(dirToGo * 0.4f, ForceMode.VelocityChange);
        //+nav
        rBody.AddForce(new_velocity, ForceMode.VelocityChange);

        //向きの和
        //+nav
        transform.rotation = a * b ;
        AddReward(-0.005f);
        swscore -= 0.005f;

        //transform.rotation = a;
        //transform.rotation *= a * b;


        //AddReward(-0.001f);


        ////Quaternion moveRotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
        //Quaternion moveRotation = Quaternion.LookRotation(desPos - myPos, Vector3.up);
        //moveRotation.z = 0;
        //moveRotation.x = 0;
        ////transform.rotation = Quaternion.Lerp(transform.rotation, moveRotation, 0.1f);
        //Quaternion a = Quaternion.Lerp(transform.rotation, moveRotation, 0.1f);

        //float forward_x = transform.forward.x * moveSpeed;
        //float forward_z = transform.forward.z * moveSpeed;

        //rBody.velocity = new Vector3(forward_x, rBody.velocity.y, forward_z);
        ////Navigation終わり

        ////Ml-Agentsの出力
        //if (action == 1) dirToGo = transform.forward;
        //if (action == 2) dirToGo = transform.forward * -2.0f;
        //if (action == 3) rotateDir = transform.up * -1.0f;
        //if (action == 4) rotateDir = transform.up;
        //transform.Rotate(rotateDir, Time.deltaTime * 200f);
        ////+nav
        //Quaternion b=Quaternion.AngleAxis(Time.deltaTime * 200f, rotateDir);

        ////速さの和
        ////+nav
        //Vector3 new_velocity = new Vector3(forward_x, rBody.velocity.y, forward_z)+(dirToGo * 0.4f);
        ////rBody.AddForce(dirToGo * 0.4f, ForceMode.VelocityChange);
        ////+nav
        //rBody.AddForce(new_velocity, ForceMode.VelocityChange);

        ////向きの和
        ////+nav
        //transform.rotation = a * b;

        // RollerAgentがTargetの位置にたどりついた時
        float distanceToTarget = Vector3.Distance(
            this.transform.position, target.position);
        if (distanceToTarget < 2.0f)
        {
            AddReward(14.0f);
            swscore += 14.0f;
            //Debug.Log("Clear");
            swresult = "Clear";
            this.transform.position = agentReset;
            EndEpisode();
        }

        // RollerAgentが床から落下した時
        if (this.transform.position.y < -0.5f)
        {
            swresult = "Miss";
            EndEpisode();
            //Debug.Log("drop");
        }
    }

    void OnDrawGizmos()
    {
        if (navMeshAgent && navMeshAgent.enabled)
        {
            Gizmos.color = Color.red;
            var prefPos = transform.position;

            foreach (var pos in navMeshAgent.path.corners)
            {
                Gizmos.DrawLine(prefPos, pos);
                prefPos = pos;
            }
        }
    }
    // ヒューリスティックモードの行動決定時に呼ばれる
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var actions = actionsOut.DiscreteActions;
        actions[0] = 0;
        actions[1] = 0;

        if (Input.GetKey(KeyCode.UpArrow)) actions[0] = 1;
        if (Input.GetKey(KeyCode.DownArrow)) actions[0] = 2;
        if (Input.GetKey(KeyCode.LeftArrow)) actions[1] = 1;
        if (Input.GetKey(KeyCode.RightArrow)) actions[1] = 2;

    }
}

//using System.Collections.Generic;
//using UnityEngine;
//using Unity.MLAgents;
//using Unity.MLAgents.Sensors;
//using Unity.MLAgents.Actuators;
//using Unity.MLAgents.Policies;
//using System;

//// RollerAgent
//public class RollerAgent : Agent
//{
//    public int agentId;
//    public GameObject ball;
//    Transform ballTf;
//    Rigidbody ballRb;
//    public override void Initialize()
//    {
//        this.ballTf = this.ball.transform;
//        this.ballRb = this.ball.GetComponent<Rigidbody>();
//    }
//    public override void CollectObservations(VectorSensor sensor)
//    {
//        float dir = (agentId == 0) ? 1.0f : -1.0f;
//        sensor.AddObservation(ballTf.localPosition.x * dir);
//        sensor.AddObservation(ballTf.localPosition.z * dir);
//        sensor.AddObservation(ballRb.velocity.x * dir);
//        sensor.AddObservation(ballRb.velocity.z * dir);
//        sensor.AddObservation(transform.localPosition.x * dir);

//    }
//    private void OnCollisionEnter(Collision collision)
//    {
//        AddReward(0.1f);
//    }
//    public override void OnActionReceived(ActionBuffers actions)
//    {
//        float dir = (agentId == 0) ? 1.0f : -1.0f;
//        int action = actions.DiscreteActions[0];
//        Vector3 pos = transform.localPosition;
//        if (action == 1)
//        {
//            pos.x -= 0.2f * dir;
//        }
//        else if (action == 2)
//        {
//            pos.x += 0.2f * dir;
//        }
//        if (pos.x < -4.0f) pos.x = -4.0f;
//        if (pos.x > 4.0f) pos.x = 4.0f;
//        transform.localPosition = pos;
//    }
//    public override void Heuristic(in ActionBuffers actionsOut)
//    {
//        var action = actionsOut.DiscreteActions;
//        action[0] = 0;
//        if (Input.GetKey(KeyCode.LeftArrow)) action[0] = 1;
//        if (Input.GetKey(KeyCode.RightArrow)) action[0] = 2;
//    }
//}

//using System.Collections.Generic;
//using UnityEngine;
//using Unity.MLAgents;
//using Unity.MLAgents.Sensors;
//using Unity.MLAgents.Actuators;
//using Unity.MLAgents.Policies;
//using System;

//// RollerAgent
//public class RollerAgent : Agent
//{
//    Rigidbody rBody;
//    int lastCheckPoint;
//    int checkPointCount;
//    public override void Initialize()
//    {
//        this.rBody = GetComponent<Rigidbody>();
//    }

//    public override void OnEpisodeBegin()
//    {
//        this.lastCheckPoint = 0;
//        this.checkPointCount = 0;
//    }
//    public override void CollectObservations(VectorSensor sensor)
//    {
//        sensor.AddObservation(rBody.velocity.x);
//        sensor.AddObservation(rBody.velocity.z);

//    }

//    public override void OnActionReceived(ActionBuffers actions)
//    {
//        Vector3 diraToGo = Vector3.zero;
//        Vector3 rotateDir = Vector3.zero;
//        int action = actions.DiscreteActions[0];
//        if (action == 1) diraToGo = transform.forward;
//        if (action == 2) diraToGo = transform.forward*-1.0f;
//        if (action == 3) rotateDir = transform.up*-1.0f;
//        if (action == 4) rotateDir = transform.up;
//        this.transform.Rotate(rotateDir, Time.deltaTime * 200f);
//        this.rBody.AddForce(diraToGo * 0.4f, ForceMode.VelocityChange);

//        AddReward(-0.001f);
//    }

//    public void EnterCheckPoint(int checkPoint)
//    {
//        if (checkPoint == (this.lastCheckPoint + 1) % 4)
//        {
//            this.checkPointCount++;
//            AddReward(0.4f);
//            if (this.checkPointCount >= 4)
//            {
//                AddReward(0.6f);
//                EndEpisode();
//            }
//        }
//        else if (checkPoint == (this.lastCheckPoint - 1 + 4) % 4)
//        {
//            this.checkPointCount--;
//            AddReward(-0.4f);
//        }
//        this.lastCheckPoint = checkPoint;
//    }
//    public override void Heuristic(in ActionBuffers actionsOut)
//    {
//        var action = actionsOut.DiscreteActions;
//        action[0] = 0;
//        if (Input.GetKey(KeyCode.UpArrow)) action[0] = 1;
//        if (Input.GetKey(KeyCode.DownArrow)) action[0] = 2;
//        if (Input.GetKey(KeyCode.LeftArrow)) action[0] = 3;
//        if (Input.GetKey(KeyCode.RightArrow)) action[0] = 4;
//    }
//}

//using System.Collections.Generic;
//using UnityEngine;
//using Unity.MLAgents;
//using Unity.MLAgents.Sensors;
//using Unity.MLAgents.Actuators;
//using Unity.MLAgents.Policies;
//using System;

//// RollerAgent
//public class RollerAgent : Agent
//{
//    Rigidbody rBody;
//    int lastCheckPoint;
//    int checkPointCount;

//    float checkPointReward;
//    float episodeReward;
//    float stepReward;

//    public void SetEnvParameter()
//    {
//        EnvironmentParameters envParams = Academy.Instance.EnvironmentParameters;
//        this.checkPointReward = envParams.GetWithDefault("checkpoint_reward", 0.0f);
//        this.episodeReward = envParams.GetWithDefault("episode_reward", 2.0f);
//        this.stepReward = envParams.GetWithDefault("step_reward", -0.001f);
//    }
//    public override void Initialize()
//    {
//        this.rBody = GetComponent<Rigidbody>();
//    }

//    public override void OnEpisodeBegin()
//    {
//        this.lastCheckPoint = 0;
//        this.checkPointCount = 0;
//        SetEnvParameter();
//    }
//    public override void CollectObservations(VectorSensor sensor)
//    {
//        sensor.AddObservation(rBody.velocity.x);
//        sensor.AddObservation(rBody.velocity.z);

//    }

//    public override void OnActionReceived(ActionBuffers actions)
//    {
//        Vector3 diraToGo = Vector3.zero;
//        Vector3 rotateDir = Vector3.zero;
//        int action = actions.DiscreteActions[0];
//        if (action == 1) diraToGo = transform.forward;
//        if (action == 2) diraToGo = transform.forward * -1.0f;
//        if (action == 3) rotateDir = transform.up * -1.0f;
//        if (action == 4) rotateDir = transform.up;
//        this.transform.Rotate(rotateDir, Time.deltaTime * 200f);
//        this.rBody.AddForce(diraToGo * 0.4f, ForceMode.VelocityChange);

//        AddReward(this.stepReward);
//    }

//    public void EnterCheckPoint(int checkPoint)
//    {
//        if (checkPoint == (this.lastCheckPoint + 1) % 4)
//        {
//            this.checkPointCount++;
//            AddReward(this.checkPointReward);
//            if (this.checkPointCount >= 4)
//            {
//                AddReward(this.episodeReward);
//                EndEpisode();
//            }
//        }
//        else if (checkPoint == (this.lastCheckPoint - 1 + 4) % 4)
//        {
//            this.checkPointCount--;
//            AddReward(-this.checkPointReward);
//        }
//        this.lastCheckPoint = checkPoint;
//    }
//    public override void Heuristic(in ActionBuffers actionsOut)
//    {
//        var action = actionsOut.DiscreteActions;
//        action[0] = 0;
//        if (Input.GetKey(KeyCode.UpArrow)) action[0] = 1;
//        if (Input.GetKey(KeyCode.DownArrow)) action[0] = 2;
//        if (Input.GetKey(KeyCode.LeftArrow)) action[0] = 3;
//        if (Input.GetKey(KeyCode.RightArrow)) action[0] = 4;
//    }
//}

//using System.Collections.Generic;
//using UnityEngine;
//using Unity.MLAgents;
//using Unity.MLAgents.Sensors;
//using Unity.MLAgents.Actuators;
//using Unity.MLAgents.Policies;
//using System;

//// RollerAgent
//public class RollerAgent : Agent
//{
//    public Transform target; // TargetのTransform
//    Rigidbody rBody; // RollerAgentのRigidBody

//    // 初期化時に呼ばれる
//    public void SetEnvParameter()
//    {
//        EnvironmentParameters envParams = Academy.Instance.EnvironmentParameters;
//        rBody.mass = envParams.GetWithDefault("mass", 1.0f);
//        var scale = envParams.GetWithDefault("scale", 1.0f);
//        rBody.gameObject.transform.localScale = new Vector3(scale, scale, scale);
//    }
//    public override void Initialize()
//    {
//        // RollerAgentのRigidBodyの参照の取得
//        this.rBody = GetComponent<Rigidbody>();
//        SetEnvParameter();
//    }

//    // エピソード開始時に呼ばれる
//    public override void OnEpisodeBegin()
//    {
//        // RollerAgentが床から落下している時
//        if (this.transform.localPosition.y < 0)
//        {
//            // RollerAgentの位置と速度をリセット
//            this.rBody.angularVelocity = Vector3.zero;
//            this.rBody.velocity = Vector3.zero;
//            this.transform.localPosition = new Vector3(0.0f, 0.5f, 0.0f);
//        }

//        // Targetの位置のリセット
//        target.localPosition = new Vector3(
//            UnityEngine.Random.value * 8 - 4, 0.5f, UnityEngine.Random.value * 8 - 4);
//    }

//    //状態取得時に呼ばれる
//    public override void CollectObservations(VectorSensor sensor)
//    {
//        sensor.AddObservation(target.localPosition.x); //TargetのX座標
//        sensor.AddObservation(target.localPosition.z); //TargetのZ座標
//        sensor.AddObservation(this.transform.localPosition.x); //RollerAgentのX座標
//        sensor.AddObservation(this.transform.localPosition.z); //RollerAgentのZ座標
//        sensor.AddObservation(rBody.velocity.x); // RollerAgentのX速度
//        sensor.AddObservation(rBody.velocity.z); // RollerAgentのZ速度
//    }

//    // 行動実行時に呼ばれる
//    public override void OnActionReceived(ActionBuffers actionBuffers)
//    {
//        // RollerAgentに力を加える
//        Vector3 controlSignal = Vector3.zero;
//        controlSignal.x = actionBuffers.ContinuousActions[0];
//        controlSignal.z = actionBuffers.ContinuousActions[1];
//        rBody.AddForce(controlSignal * 10);

//        // RollerAgentがTargetの位置にたどりついた時
//        float distanceToTarget = Vector3.Distance(
//            this.transform.localPosition, target.localPosition);
//        if (distanceToTarget < 1.42f)
//        {
//            AddReward(1.0f);
//            EndEpisode();
//        }

//        // RollerAgentが床から落下した時
//        if (this.transform.localPosition.y < 0)
//        {
//            EndEpisode();
//        }
//    }

//    // ヒューリスティックモードの行動決定時に呼ばれる
//    public override void Heuristic(in ActionBuffers actionsOut)
//    {
//        var continuousActionsOut = actionsOut.ContinuousActions;
//        continuousActionsOut[0] = Input.GetAxis("Horizontal");
//        continuousActionsOut[1] = Input.GetAxis("Vertical");
//    }
//}

//using System.Collections.Generic;
//using UnityEngine;
//using Unity.MLAgents;
//using Unity.MLAgents.Sensors;
//using Unity.MLAgents.Actuators;
//using Unity.MLAgents.Policies;

//// RollerAgent
//public class RollerAgent : Agent
//{
//    Rigidbody rBody;
//    public Transform[] targets;
//    public GameObject[] boards;
//    int boardId;

//    public override void Initialize()
//    {
//        this.rBody = GetComponent<Rigidbody>();
//    }

//    public override void OnEpisodeBegin()
//    {
//        this.rBody.angularVelocity = Vector3.zero;
//        this.rBody.velocity = Vector3.zero;
//        this.transform.localPosition = new Vector3(0.0f, 0.5f, -5.5f);
//        this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

//        this.boardId = Random.Range(0, 2);
//        this.boards[0].SetActive(boardId == 0);
//        this.boards[1].SetActive(boardId == 1);
//        if (Random.Range(0, 2) == 0)
//        {
//            this.targets[0].localPosition = new Vector3(-3f, 0.5f, 5f);
//            this.targets[1].localPosition = new Vector3(3f, 0.5f, 5f);
//        }
//        else
//        {
//            this.targets[0].localPosition = new Vector3(3f, 0.5f, 5f);
//            this.targets[1].localPosition = new Vector3(-3f, 0.5f, 5f);
//        }
//    }


//    public override void OnActionReceived(ActionBuffers actions)
//    {
//        Vector3 diraToGo = Vector3.zero;
//        Vector3 rotateDir = Vector3.zero;
//        int action = actions.DiscreteActions[0];
//        if (action == 1) diraToGo = transform.forward;
//        if (action == 2) diraToGo = transform.forward * -1.0f;
//        if (action == 3) rotateDir = transform.up * -1.0f;
//        if (action == 4) rotateDir = transform.up;
//        this.transform.Rotate(rotateDir, Time.deltaTime * 200f);
//        this.rBody.AddForce(diraToGo * 0.4f, ForceMode.VelocityChange);

//        for (int i = 0; i < 2; i++)
//        {
//            float distanceToTarget = Vector3.Distance(
//                this.transform.localPosition, targets[i].localPosition);
//            if(distanceToTarget < 1.42f)
//            {
//                if (i == boardId)
//                {
//                    AddReward(1.0f);
//                }
//                EndEpisode();
//            }
//        }
//        AddReward(-0.0005f);
//    }

//    public override void Heuristic(in ActionBuffers actionsOut)
//    {
//        var action = actionsOut.DiscreteActions;
//        action[0] = 0;
//        if (Input.GetKey(KeyCode.UpArrow)) action[0] = 1;
//        if (Input.GetKey(KeyCode.DownArrow)) action[0] = 2;
//        if (Input.GetKey(KeyCode.LeftArrow)) action[0] = 3;
//        if (Input.GetKey(KeyCode.RightArrow)) action[0] = 4;
//    }
//}
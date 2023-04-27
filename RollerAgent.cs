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

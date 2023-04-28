using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine.AI;
using System.IO;
using System.Text;
using System.Linq;
using System;

//+nav
[RequireComponent(typeof(NavMeshAgent))]
// RollerAgent
public class RollerAgent : Agent
{
    public Transform target; // Target��Transform
    Rigidbody rBody; // RollerAgent��RigidBody
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
    public Transform[] startPointsRight;
    public Transform[] startPointsLeft;
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
    private StreamWriter swtest;
    private int swstep;
    private int sweps;
    private float swscore;
    private string swresult;
    private float ypos;
    private int firstrand;
    private DateTime starttime;
    private DateTime endtime;
    private bool agentStop;
    private int personnumR;
    private int personnumL;
    private string filename1;
    private string filename2;

    private void Start()
    {
        //FileStream fs = File.OpenWrite("Assets/Logs/SaveData.csv");
        //sw = new StreamWriter("Assets/Logs/SaveData.csv", true, Encoding.GetEncoding("Shift_JIS"));
        //sw = new StreamWriter("SaveData.txt", false, Encoding.GetEncoding("Shift_JIS"));
        filename1="Assets/Logs/StopOrSlidemove.csv";
        filename2="Assets/Logs/StopOrSlidemove-time.csv";

        personnumR=0;
        personnumL=0;
        swstep =0;
        sweps=0;
        swscore = 0f;
        swresult = "start";
        ypos = 1.0f;
        agentStop=false;

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
        starttime=DateTime.Now;
    }

    // ���������ɌĂ΂��
    public override void Initialize()
    {
        // RollerAgent��RigidBody�̎Q�Ƃ̎擾
        this.rBody = GetComponent<Rigidbody>();
        //+nav
        navMeshAgent = this.GetComponent<NavMeshAgent>();
    }

    // �G�s�\�[�h�J�n���ɌĂ΂��
    public override void OnEpisodeBegin()
    {
        sw = new StreamWriter(filename1, true, Encoding.GetEncoding("Shift_JIS"));
        string memo = sweps.ToString()+":"+ swstep.ToString() + ":" + swscore.ToString() + ":" + swresult;
        sw.WriteLine(memo);
        //sw.Flush();
        sw.Close();
        Debug.Log(memo);
        swstep = 0;
        sweps += 1;
        swscore = 0f;
        // RollerAgent�������痎�����Ă��鎞
        //if (this.transform.localPosition.y < 0)
        //{
        // RollerAgent�̈ʒu�Ƒ��x�����Z�b�g
        this.rBody.angularVelocity = Vector3.zero;
        this.rBody.velocity = Vector3.zero;
        //this.transform.position = new Vector3(100.2f, 0.35f, 127.1f);
        rot = 0;
        //firstrand = Random.Range(0, 2);
        firstrand=0;
        if(firstrand==0){
            UpDn = UnityEngine.Random.Range(0, 2);
            //UpDn = 0;
            agentInt = UnityEngine.Random.Range(0, startPointsDown.Length);
            targetInt = UnityEngine.Random.Range(0, startPointsUp.Length);
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
        }
        else{
            UpDn = UnityEngine.Random.Range(0, 2);
            //UpDn = 0;
            agentInt = UnityEngine.Random.Range(0, startPointsLeft.Length);
            targetInt = UnityEngine.Random.Range(0, startPointsRight.Length);

            if (UpDn == 0)
            {
                this.transform.position = startPointsLeft[agentInt].position;
                agentReset = this.transform.position;
                target.position = startPointsRight[targetInt].position;
                //target.position.y = ypos;
            }
            else
            {
                this.transform.position = startPointsRight[agentInt].position;
                agentReset = this.transform.position;
                target.position =startPointsLeft[targetInt].position;
            }
        }

        this.transform.position = agentReset;

        for (int i = 0; i < players.Length; i++)
        {
            navmeshs[i].isStopped = true;
            players[i].transform.position= players_position[i];
            var ind = UnityEngine.Random.Range(0, waypoints.Length);
            navmeshs[i].SetDestination(waypoints[ind].position);
            navmeshs[i].isStopped = false;
        }
        //�G�[�W�F���g�̏����ʒu
        //Debug.Log(agentReset);
        //Debug.Log(this.transform.position);
        //Debug.Log(this.transform.localPosition);
        position = true;
        beforeArcb.y = 0;
        moveRotation.y = 0;
        //this.transform.position = agentReset;
    }

    ////��Ԏ擾���ɌĂ΂��
    public override void CollectObservations(VectorSensor sensor)
    {
        Transform myTransform = this.transform;
        Vector3 myPos = myTransform.position;
        Vector3 desPos;
        myPos.y = 0.3f;
        moveRotation.y = 0.0f;
        //sensor.AddObservation(target.localPosition.x); //Target��X���W
        //sensor.AddObservation(target.localPosition.z); //Target��Z���W
        //sensor.AddObservation(this.transform.localPosition.x); //RollerAgent��X���W
        //sensor.AddObservation(this.transform.localPosition.z); //RollerAgent��Z���W
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
        sensor.AddObservation(rBody.velocity.x); // RollerAgent��X���x
        sensor.AddObservation(rBody.velocity.z); // RollerAgent��Z���x
        sensor.AddObservation(this.transform.rotation.y);
        sensor.AddObservation(moveRotation.y);
        //視線情報１２要素分
        //sensor.AddObservation(arclist);
        swstep += 1;
        if (m_StepCount >= MaxStep-1)
        {
            swresult = "MissOver";
            //AddReward(-2.0f);
        }
        
        //sensor.AddObservation(beforeArcb.y);
        //Debug.Log("sensor" + moveRotation.y);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Human")
        {
            //-2.0
            Debug.Log("Miss");
            AddReward(-4.0f);
            swscore -= 4.0f;
            swresult = "Miss";
            if(agentStop){
                swresult = "MissByHuman";
            }

            swtest = new StreamWriter(filename2, true, Encoding.GetEncoding("Shift_JIS"));
            endtime=DateTime.Now;
            var difftime=endtime-starttime;
            string timeresult = difftime.ToString();
            string testmemo=sweps.ToString()+": CollisionTime: "+timeresult;
            swtest.WriteLine(testmemo);
            //swtest.Flush();
            swtest.Close();
            EndEpisode();
        }       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Checkpoint")
        {
            //0.2
            //Debug.Log("checkpoint");
            AddReward(0.5f);
            swscore += 0.5f;
            //once = 2;
        }
        if (other.gameObject.tag == "Checkout")
        {
            //-0.6
            //Debug.Log("danger");
            AddReward(-0.4f);
            swscore -= 0.4f;
        }
    }


    // �s�����s���ɌĂ΂��
    public override void OnActionReceived(ActionBuffers vectorActions)
    {
        //Debug.Log("action");
        //Debug.Log(this.transform.position);
        //Debug.Log(this.transform.localPosition);
        if (position)
        {
            this.transform.position = agentReset;
            position = false;
            // if(this.transform.position.x>=-110.0f || this.transform.position.x<=-200.0f){
            // this.transform.position=new Vector3(-150.0f,this.transform.position.y,this.transform.position.z);
            // }
            // if(this.transform.position.z>=110.0f){
            //     this.transform.position=new Vector3(this.transform.position.x,this.transform.position.y,105.0f);
            // }
            // if(this.transform.position.z<=52.0f){
            //     this.transform.position=new Vector3(this.transform.position.x,this.transform.position.y,60.0f);
            // }
        }
        agentStop=false;
        
        // RollerAgent�ɗ͂�������
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
        //Navigation�I���
        
        //Ml-Agents�̏o��
        if (action1 == 1) {
            dirToGo = transform.forward * moveSpeed * -1.0f;
            //-0.0002
            AddReward(-0.0004f);
            swscore -= 0.0004f;
        }
        if (action1 == 2) { 
            dirToGo = transform.forward * moveSpeed * -2.0f;
            //-0.0004
            AddReward(-0.0008f);
            swscore -= 0.0008f;
            agentStop=true;
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
        
        //�����̘a
        //+nav
        
        //rBody.AddForce(dirToGo * 0.4f, ForceMode.VelocityChange);

        Vector3 new_velocity = new Vector3(forward_x, rBody.velocity.y, forward_z) + (dirToGo);
        //stopraycast
        personnumR=0;
        personnumL=0;

        var direction = this.transform.forward;
        var rayDistance = 2.0f;
        Vector3 rayPosition = transform.position + new Vector3(0.0f, 0.5f, 0.0f);
        Ray ray = new Ray(rayPosition, direction);
        Debug.DrawRay(rayPosition, direction * rayDistance, Color.green);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            //Debug.Log(hit.collider.gameObject.tag);
            if(hit.collider.gameObject.CompareTag("Human"))
            {
              //dirToGo = transform.forward * moveSpeed * -1.0f;
               rotateDir = transform.up * -1.0f;
               rot += 24;
               personnumR+=1;
              //this.transform.Rotate(0f,15.0f,0f,Space.World);
              //new_velocity=new Vector3(0.0f,0.0f,0.0f);
              
              //StartCoroutine("HumanStop");
            }
        }

        direction = ((transform.forward*5.6712f)+transform.right).normalized;       
        //rayDistance = 2.0f;
        rayDistance = 5.0f;
        rayPosition = transform.position + new Vector3(0.0f, 0.5f, 0.0f);
        ray = new Ray(rayPosition, direction);
        Debug.DrawRay(rayPosition, direction * rayDistance, Color.green);
        //RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            //Debug.Log(hit.collider.gameObject.tag);
            if(hit.collider.gameObject.CompareTag("Human"))
            {
              //dirToGo = transform.forward * moveSpeed * -1.0f;
              rotateDir = transform.up*-1.0f;
              rot += 24;  
              personnumR+=1;
              //this.transform.Rotate(0f,15.0f,0f,Space.World);
              //new_velocity=new Vector3(0.0f,0.0f,0.0f);
              
              //StartCoroutine("HumanStop");
            }
        }

        direction = ((transform.forward*5.6712f)+transform.right*-1).normalized;       
        
        rayPosition = transform.position + new Vector3(0.0f, 0.5f, 0.0f);
        ray = new Ray(rayPosition, direction);
        Debug.DrawRay(rayPosition, direction * rayDistance, Color.green);
        //RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            //Debug.Log(hit.collider.gameObject.tag);
            if(hit.collider.gameObject.CompareTag("Human"))
            {
              //dirToGo = transform.forward * moveSpeed * -1.0f;
               rotateDir = transform.up*-1.0f;
               rot -= 24;
               personnumL+=1;
              //this.transform.Rotate(0f,15.0f,0f,Space.World);
              //new_velocity=new Vector3(0.0f,0.0f,0.0f);
              
              //StartCoroutine("HumanStop");
            }
        }

        direction = ((transform.forward*2.7474f)+transform.right).normalized;       
        
        rayPosition = transform.position + new Vector3(0.0f, 0.5f, 0.0f);
        ray = new Ray(rayPosition, direction);
        Debug.DrawRay(rayPosition, direction * rayDistance, Color.green);
        //RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            //Debug.Log(hit.collider.gameObject.tag);
            if(hit.collider.gameObject.CompareTag("Human"))
            {
              //dirToGo = transform.forward * moveSpeed * -1.0f;
               rotateDir = transform.up * -1.0f;
               rot += 24;
               personnumR+=1;
              //this.transform.Rotate(0f,15.0f,0f,Space.World);
              //new_velocity=new Vector3(0.0f,0.0f,0.0f);
              
              //StartCoroutine("HumanStop");
            }
        }

        direction = ((transform.forward*2.7474f)+transform.right*-1).normalized;       
        
        rayPosition = transform.position + new Vector3(0.0f, 0.5f, 0.0f);
        ray = new Ray(rayPosition, direction);
        Debug.DrawRay(rayPosition, direction * rayDistance, Color.green);
        //RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            //Debug.Log(hit.collider.gameObject.tag);
            if(hit.collider.gameObject.CompareTag("Human"))
            {
              //dirToGo = transform.forward * moveSpeed * -1.0f;
               rotateDir = transform.up*-1.0f;
               rot -= 24;
               personnumL+=1;
              //this.transform.Rotate(0f,15.0f,0f,Space.World);
              //new_velocity=new Vector3(0.0f,0.0f,0.0f);
              //StartCoroutine("HumanStop");
            }
        }

        direction = ((transform.forward*1.7320f)+transform.right).normalized;       
        
        rayPosition = transform.position + new Vector3(0.0f, 0.5f, 0.0f);
        ray = new Ray(rayPosition, direction);
        Debug.DrawRay(rayPosition, direction * rayDistance, Color.green);
        //RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            //Debug.Log(hit.collider.gameObject.tag);
            if(hit.collider.gameObject.CompareTag("Human"))
            {
              //dirToGo = transform.forward * moveSpeed * -1.0f;
               rotateDir = transform.up * -1.0f;
               rot += 24;
               personnumR+=1;
              //this.transform.Rotate(0f,15.0f,0f,Space.World);
              //new_velocity=new Vector3(0.0f,0.0f,0.0f);
              
              //StartCoroutine("HumanStop");
            }
        }

        direction = ((transform.forward*1.7320f)+transform.right*-1).normalized;       
       
        rayPosition = transform.position + new Vector3(0.0f, 0.5f, 0.0f);
        ray = new Ray(rayPosition, direction);
        Debug.DrawRay(rayPosition, direction * rayDistance, Color.green);
        //RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            //Debug.Log(hit.collider.gameObject.tag);
            if(hit.collider.gameObject.CompareTag("Human"))
            {
              //dirToGo = transform.forward * moveSpeed * -1.0f;
               rotateDir = transform.up*-1.0f;
               rot -= 24;
               personnumL+=1;
              //this.transform.Rotate(0f,15.0f,0f,Space.World);
              //new_velocity=new Vector3(0.0f,0.0f,0.0f);
              //StartCoroutine("HumanStop");
            }
        }

        direction = ((transform.forward*1.1917f)+transform.right).normalized;       
        
        rayPosition = transform.position + new Vector3(0.0f, 0.5f, 0.0f);
        ray = new Ray(rayPosition, direction);
        Debug.DrawRay(rayPosition, direction * rayDistance, Color.green);
        //RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            //Debug.Log(hit.collider.gameObject.tag);
            if(hit.collider.gameObject.CompareTag("Human"))
            {
              //dirToGo = transform.forward * moveSpeed * -1.0f;
               rotateDir = transform.up * -1.0f;
               rot += 24;
               personnumR+=1;
              //this.transform.Rotate(0f,15.0f,0f,Space.World);
              //new_velocity=new Vector3(0.0f,0.0f,0.0f);
              
              //StartCoroutine("HumanStop");
            }
        }

        direction = ((transform.forward*1.1917f)+transform.right*-1).normalized;       
       
        rayPosition = transform.position + new Vector3(0.0f, 0.5f, 0.0f);
        ray = new Ray(rayPosition, direction);
        Debug.DrawRay(rayPosition, direction * rayDistance, Color.green);
        //RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            //Debug.Log(hit.collider.gameObject.tag);
            if(hit.collider.gameObject.CompareTag("Human"))
            {
              //dirToGo = transform.forward * moveSpeed * -1.0f;
               rotateDir = transform.up*-1.0f;
               rot -= 24;
               personnumL+=1;
              //this.transform.Rotate(0f,15.0f,0f,Space.World);
              //new_velocity=new Vector3(0.0f,0.0f,0.0f);
              //StartCoroutine("HumanStop");
            }
        }
        if(personnumL>=1 && personnumR>=1){
            new_velocity=new Vector3(0.0f,0.0f,0.0f);
        }


        //+nav
        Quaternion b = Quaternion.AngleAxis(Time.deltaTime * 3.0f * rot, rotateDir);
        beforeArcb = b;
        
        transform.rotation = a * b ;
        rBody.AddForce(new_velocity, ForceMode.VelocityChange);

        //�����̘a
        //+nav
        
        //-0.0004
        AddReward(-0.004f);
        swscore -= 0.004f;

        //transform.rotation = a;
        //transform.rotation *= a * b;
        
        // RollerAgent��Target�̈ʒu�ɂ��ǂ������
        float distanceToTarget = Vector3.Distance(
            this.transform.position, target.position);
        if (distanceToTarget < 2.0f)
        {
            //7
            AddReward(14.0f);
            swscore += 14.0f;
            //Debug.Log("Clear");
            swresult = "Clear";
            this.transform.position = agentReset;
            EndEpisode();
        }

        // RollerAgent�������痎��������
        if (this.transform.position.y < -0.5f)
        {
            swresult = "Drop";
            EndEpisode();
            //Debug.Log("drop");
        }
        // if(this.transform.position.z<=35.0f ||this.transform.position.z>=140.0f){
        //     Debug.Log("OutPosition");
        //     swresult = "OutPosition";
        //     EndEpisode();
        // }
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
    // �q���[���X�e�B�b�N���[�h�̍s�����莞�ɌĂ΂��
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

    IEnumerator HumanStop()
    {
        yield return new WaitForSeconds(1.5f);
        Debug.Log("stop");
    }
}


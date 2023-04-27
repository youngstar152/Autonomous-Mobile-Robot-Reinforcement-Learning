using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// NavMeshAgent�R���|�[�l���g���A�^�b�`����Ă��Ȃ��ꍇ�A�^�b�`
[RequireComponent(typeof(NavMeshAgent))]
public class WaypointMove : MonoBehaviour
{
    [SerializeField]
    [Tooltip("���񂷂�n�_�̔z��")]
    private Transform[] waypoints;

    // NavMeshAgent�R���|�[�l���g������ϐ�
    private NavMeshAgent navMeshAgent;
    // ���݂̖ړI�n
    private int currentWaypointIndex;
    private int point;
    private int pointFirst;

    // Start is called before the first frame update
    void Start()
    {
        pointFirst = Random.Range(0, waypoints.Length);
        // navMeshAgent�ϐ���NavMeshAgent�R���|�[�l���g������
        navMeshAgent = GetComponent<NavMeshAgent>();
        // �ŏ��̖ړI�n������
        navMeshAgent.SetDestination(waypoints[pointFirst].position);
        var spe = Random.Range(4, 10);
        navMeshAgent.speed = spe * 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        // �ړI�n�_�܂ł̋���(remainingDistance)���ړI�n�̎�O�܂ł̋���(stoppingDistance)�ȉ��ɂȂ�����
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            point = Random.Range(0, waypoints.Length);
            // �ړI�n�̔ԍ����P�X�V�i�E�ӂ���]���Z�q�ɂ��邱�ƂŖړI�n�����[�v�������j
            currentWaypointIndex = (currentWaypointIndex + point) % waypoints.Length;
            // �ړI�n�����̏ꏊ�ɐݒ�
            navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }
}

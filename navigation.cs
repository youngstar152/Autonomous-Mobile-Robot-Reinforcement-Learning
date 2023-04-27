using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class navigation : MonoBehaviour
{
    //[SerializeField]
    //private Transform m_target;
    //private NavMeshAgent m_agent;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    m_agent = this.GetComponent<NavMeshAgent>();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    m_agent.SetDestination(m_target.position);
    //}

    protected Rigidbody rb;
    protected NavMeshAgent navMeshAgent;
    public Transform m_target;
    public float moveSpeed;
    

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        //moveSpeed = 1.0f;
    }

    void Update()
    {
        // Ç±Ç±Ç≈DoMoveÇåƒÇ—èoÇ∑èàóù
        DoMove(m_target.position);
    }

    private void DoMove(Vector3 targetPosition)
    {
        if (navMeshAgent && navMeshAgent.enabled)
        {
            navMeshAgent.SetDestination(targetPosition);

            foreach (var pos in navMeshAgent.path.corners)
            {
                var diff2d = new Vector2(
                    Mathf.Abs(pos.x - transform.position.x),
                    Mathf.Abs(pos.z - transform.position.z)
                );

                if (0.1f <= diff2d.magnitude)
                {
                    targetPosition = pos;
                    break;
                }
            }

            Debug.DrawLine(transform.position, targetPosition, Color.red);
        }

        Quaternion moveRotation = Quaternion.LookRotation(targetPosition - transform.position, Vector3.up);
        moveRotation.z = 0;
        moveRotation.x = 0;
        transform.rotation = Quaternion.Lerp(transform.rotation, moveRotation, 0.1f);

        float forward_x = transform.forward.x * moveSpeed;
        float forward_z = transform.forward.z * moveSpeed;

        rb.velocity = new Vector3(forward_x, rb.velocity.y, forward_z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("hit");
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if()
    //}
}

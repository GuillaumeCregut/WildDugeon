using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1Behaviour : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;
    public Transform pointD;
    public Transform pointE;

    public UnityEngine.AI.NavMeshAgent agent; 
    
    private string pathStatus="d";
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Transform>().LookAt(pointB);
        agent.SetDestination(pointB.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name=="PointPatrolA")
        {
            GetComponent<Transform>().LookAt(pointB);
            agent.SetDestination(pointB.transform.position);
            pathStatus="d";
        }
         if(other.name=="PointPatrolB")
        {
            if(pathStatus=="d")
            {
                GetComponent<Transform>().LookAt(pointC);
                //GetComponent<Rigidbody>.velocity=transform.forward*2;
                agent.SetDestination(pointC.transform.position);
            }
            else
            {
                GetComponent<Transform>().LookAt(pointA);
                agent.SetDestination(pointA.transform.position);
            }
        }
         if(other.name=="PointPatrolC")
        {
             pathStatus="u";
             GetComponent<Transform>().LookAt(pointB);
                agent.SetDestination(pointB.transform.position);
        }
    }
}

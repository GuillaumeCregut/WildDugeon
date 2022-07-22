using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster2Behaviour : MonoBehaviour
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
        GetComponent<Transform>().LookAt(pointD);
        agent.SetDestination(pointD.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name=="PointPatrolA")
        {
            GetComponent<Transform>().LookAt(pointD);
            agent.SetDestination(pointD.transform.position);
            pathStatus="d";
        }
         if(other.name=="PointPatrolD")
        {
            if(pathStatus=="d")
            {
                GetComponent<Transform>().LookAt(pointE);
                //GetComponent<Rigidbody>.velocity=transform.forward*2;
                agent.SetDestination(pointE.transform.position);
            }
            else
            {
                GetComponent<Transform>().LookAt(pointA);
                agent.SetDestination(pointA.transform.position);
            }
        }
         if(other.name=="PointPatrolE")
        {
             pathStatus="u";
             GetComponent<Transform>().LookAt(pointD);
                agent.SetDestination(pointD.transform.position);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AgentMovement : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public GameObject player;
     AudioSource m_audio;
   
    Animator animator;
    const string STAND_STATE="Stand";
    const string WALK_STATE="Walk";
    const string ATTACK_STATE="Attack";

//Actual Action
    public string currentAction;
 
    private void Awake()
    {
       currentAction = STAND_STATE;
       
    }

    public void Shout()
    {
        m_audio.Play();
        animator.ResetTrigger("ToShout");
    }
    // Start is called before the first frame update
    void Start()
    {
        m_audio=GetComponent<AudioSource>();
         agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        Walk();
    }


    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.M))
        {
         //   animator.SetBool("Walk", true);
            Debug.Log(animator.GetBool("Walk"));
            
        }
     
        if (Input.GetKeyDown(KeyCode.G))
        {
            animator.SetTrigger("ToShout");

            Debug.Log("Must Shout");
           
        }
        if(player !=null)
        {
            if(MovingToTarget())
            {
                return;
            }
            else{
                Attack();
            }
        }
    }

    bool MovingToTarget()
    {
        agent.SetDestination(player.transform.position);
        if(agent.remainingDistance==0)
            return false;
        if(agent.remainingDistance>agent.stoppingDistance)
        {
            if(currentAction !=WALK_STATE)
                Walk();
        }
        else
        {
            RotateToTarget(player.transform);
            return false;
        }
        return true;
    }

    void Walk()
    {
        ResetAnimation();
        currentAction=WALK_STATE;
        animator.SetBool(WALK_STATE,true);
    }
      
    void Attack()
    {
        ResetAnimation();
        currentAction=ATTACK_STATE;
        animator.SetBool(ATTACK_STATE,true);
         Debug.Log("Attack");
    }

    private void ResetAnimation()
    {
        animator.SetBool(WALK_STATE,false);
        animator.SetBool(ATTACK_STATE,false);
    }
    
    private void RotateToTarget(Transform target)
    {
        Vector3 direction=(target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0,direction.z));
        transform.rotation=Quaternion.Slerp(transform.rotation,lookRotation, Time.deltaTime *3f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AgentMovement : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;

    public GameObject player;
    AudioSource m_audio;
    public AudioClip cry;
    Animator animator;
    const string STAND_STATE = "Stand";
    const string WALK_STATE = "Walk";
    const string ATTACK_STATE = "Attack";

    //Actual Action
    public string currentAction;

    private void Awake()
    {
        currentAction = STAND_STATE;
        m_audio = GetComponent<AudioSource>();
        Debug.Log("Awake");
    }

    public void Shout()
    {
        Debug.Log("Shout Function");
        if (!m_audio.isPlaying)
            m_audio.PlayOneShot(cry, 1);
        animator.ResetTrigger("ToShout");
    }
    // Start is called before the first frame update
    void Start()
    {

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        Debug.Log("Start");
        m_audio.Stop();
        Walk();
    }


    // Update is called once per frame
    void Update()
    {
       
        int rand_num = Random.Range(0, 20);
         Debug.Log(rand_num);
        if (rand_num == 5)
        {
              Debug.Log("Animation");
            animator.SetTrigger("ToShout");
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            //   animator.SetBool("Walk", true);
            Debug.Log(animator.GetBool("Walk"));

        }

        if (Input.GetKeyDown(KeyCode.G))
        {

            if (m_audio.isPlaying)
                Debug.Log("En cours");

            Debug.Log("Must Shout");
            if (!m_audio.isPlaying)
            m_audio.PlayOneShot(cry, 1);
        }
        if (player != null)
        {
            if (MovingToTarget())
            {
                return;
            }
            else
            {
                Attack();
            }

        }
    }

    bool MovingToTarget()
    {
      //   float distance = Vector3.Distance(agent.transform.position, player.transform.position);
      double distance=3.1;
        if (distance<3.0)
        {
            agent.SetDestination(player.transform.position);
                Debug.Log(agent.remainingDistance);
                if (agent.remainingDistance == 0)
                {
                    return false;
                }
                if (agent.remainingDistance > agent.stoppingDistance)
                {
                    if (currentAction != WALK_STATE)
                        Walk();
                }
                else
                {
                    RotateToTarget(player.transform);
                    return false;
                }
                return true;
            }
        else
             return true;
    }

    void Walk()
    {
        ResetAnimation();
        currentAction = WALK_STATE;
        animator.SetBool(WALK_STATE, true);
    }

    void Attack()
    {
        ResetAnimation();
        currentAction = ATTACK_STATE;
        animator.SetBool(ATTACK_STATE, true);
        Debug.Log("Attack");
    }

    private void ResetAnimation()
    {
        animator.SetBool(WALK_STATE, false);
        animator.SetBool(ATTACK_STATE, false);
    }

    private void RotateToTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3f);
    }
}

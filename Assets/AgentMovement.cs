using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform player;
     AudioSource m_audio;
    private bool playRoar;
    private Animator animator;

    /*
        public float health;*/

        public LayerMask whatIsGround, whatIsPlayer;

        //Patrolling
        public Vector3 walkPoint;
        bool walkPointSet;
        public float walkPointRange;

        //Attacking
        public float timeBetweenAttack;
        bool alreadyAttacked;
        public GameObject projectile; //A supprimer ou modifier

        //States
      /*  public float sightRange, attackRange;
        public bool playerInSightRange, playerInAttackRange;*/
    private void Awake()
    {
        /*player=GameObject.Find("Dwarf BerserkerM").transform; //Voir s'il faut pas changer le nom
        agent=GetComponent<UnityEngine.AI.NavMeshAgent>();*/
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
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
        Patroling();
    }
public void PrintEvent(string s)
    {
        Debug.Log("PrintEvent: " + s + " called at: " + Time.time);
    }

    // Update is called once per frame
    void Update()
    {
        /* playerInSightRange=Physics.CheckSphere(transform.position,sightRange,whatIsPlayer);
         playerInAttackRange=Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
         if (!playerInSightRange && !playerInAttackRange)*/
          Patroling();
        /* if (playerInSightRange && !playerInAttackRange) ChasePlayer();
         if (playerInAttackRange && playerInSightRange) AttackPlayer();*/
        /* Vector2 input =new Vector2(Input.GetAxis("Horizontal"), Input.getAxis("Vertical"));*/
        if (Input.GetKeyDown(KeyCode.M))
        {
         //   animator.SetBool("Walk", true);
            Debug.Log(animator.GetBool("Walk"));
            
        }
      /*  if (Input.GetKeyDown(KeyCode.L))
        {
            animator.SetBool("Walk", false);
            Debug.Log(animator.GetBool("Walk"));
        }*/
        if (Input.GetKeyDown(KeyCode.G))
        {
            animator.SetTrigger("ToShout");

            Debug.Log("Must Shout");
           
           //m_audio.Play();
           // Debug.Log(animator.GetBool("Walk"));
        }
    }
    
        private void Patroling()
        {
            if (!walkPointSet) SearchWalkPoint();
            Debug.Log("Start Partol");
            if (walkPointSet)
                agent.SetDestination(walkPoint);

            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            //Walkpoint reached
            if (distanceToWalkPoint.magnitude < 1f)
                walkPointSet = false;
        }
        private void SearchWalkPoint()
        {
            //Calculate random point in range
            float randomZ = Random.Range(-walkPointRange, walkPointRange);
            float randomX = Random.Range(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

            if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
                walkPointSet = true;
        }
/*
        private void ChasePlayer()
        {
            agent.SetDestination(player.position);
        }

         private void AttackPlayer()
        {

        }

        public void TakeDamage(int damage)
        {
            health -= damage;

            if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
        }
        private void DestroyEnemy()
        {
            Destroy(gameObject);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sightRange);
        }
    */
}

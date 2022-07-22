using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
 
public class FightMonster : MonoBehaviour
{
 
    public GameObject player;
 
    public int health;
    public int damage;

    //Agent de Navigation
    NavMeshAgent navMeshAgent;
    
 
    //Animations
    Animator animator;
    const string STAND_STATE = "Stand";
    //const string WALK_STATE = "Walk";
    const string ATTACK_STATE = "Attack";

    //Action actuelle
    public string currentAction;
 
 
    // Start is called before the first frame update
    void Awake()
    {
        currentAction = STAND_STATE;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();      
    }
 
    // Update is called once per frame
    void Update()
    {

        if (player != null)
        {
        float distance = Vector3.Distance(navMeshAgent.transform.position, player.transform.position);
        if (distance<1){
         RotateToTarget(player.transform);
         Attack();
        }
            //Est-ce que l'IA se déplace vers le joueur ?
        /*
            if (MovingToTarget())
            {
                //En train de marcher
                return;
            }

            //Sinon c'est qu'elle est à distance d'attaque
            else
            {
                Attack();
            }
        */
        }
    }
 
 /*
    bool MovingToTarget()
    {
        //Assigne la destination : le joueur
        navMeshAgent.SetDestination(player.transform.position);
 
        //navMeshAgent pas prêt ?
        if (navMeshAgent.remainingDistance == 0)
            return false;
 
 
        // navMeshAgent.remainingDistance = distance restante pour atteindre la cible (Player)
        // navMeshAgent.stoppingDistance = à quel distance de la cible l'IA doit s'arrête 
        // (exemple 2 m pour le corps à sorps) 

        if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
        {

            if (currentAction != WALK_STATE)
                Walk();
 
        }
        else
        {
            //Si arrivé à bonne distance, regarde vers le joueur
            RotateToTarget(player.transform);
            return false;
        }
 
        return true;
    }
    */
 
 /*
    //Walk = Marcher
    void Walk()
    {
        //Réinitialise les paramètres de l'animator
        ResetAnimation();
        //L'action est maintenant "Walk"
        currentAction = WALK_STATE;
        //Le paramètre "Walk" de l'animator = true
        animator.SetBool(WALK_STATE, true);
    }
*/ 
    //Attack = Attaquer
    void Attack()
    {
        Debug.Log("AttackMonster");
        //Réinitialise les paramètres de l'animator
        ResetAnimation();
        //L'action est maintenant "Attack"
        currentAction = ATTACK_STATE;
        //Le paramètre "Attack" de l'animator = true
        animator.SetBool(ATTACK_STATE, true);
    }
 
    private void ResetAnimation()
    {  
    //    animator.SetBool(WALK_STATE, false);
        animator.SetBool(ATTACK_STATE, false);
    }
 
 
    //Permet de tout le temps regarder en direction de la cible
    private void RotateToTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3f);
    }
}
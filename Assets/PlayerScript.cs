using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
//    AudioSource MyAudioSource;
    //AudioClip Sfx;

    public float speed;
    public int rotationSpeed;

    public GameObject ennemy;
    public int health;
    public int damage;
    private Animator otherAnimator;

    //Animations
    Animator animator;
    const string STAND_STATE = "Stand";
    //const string WALK_STATE = "Walk";
    const string ATTACK_STATE = "Attack";
    const string DEAD_STATE = "Dead";

    //Action actuelle
    public string currentAction;
 
 
    // Start is called before the first frame update
    void Awake()
    {
        currentAction = STAND_STATE;
        animator = GetComponent<Animator>();      
    }


    // Start is called before the first frame update
    void Start()
    {
    //    MyAudioSource = GetComponent<AudioSource>();
    //    MyAudioSource.Play();
    }

    void Attack()
    {
        Debug.Log("AttackPlayer");
        //Réinitialise les paramètres de l'animator
        ResetAnimation();
        //L'action est maintenant "Attack"
        currentAction = ATTACK_STATE;
        //Le paramètre "Attack" de l'animator = true
        animator.SetBool(ATTACK_STATE, true);
        TakeDamage(damage);
    }
 
    private void ResetAnimation()
    {  
    //    animator.SetBool(WALK_STATE, false);
        animator.SetBool(ATTACK_STATE, false);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        //Debug.Log("health", health);
        //Debug.Log("damage", damage);
        Debug.Log(health);
        Debug.Log(damage);
        //if (health <= 0) animator.SetBool(DEAD_STATE, true);
        if (health <= 0) Invoke(nameof(DestroyEnnemy), 2f);
    }

    private void DestroyEnnemy()
    {
        otherAnimator = ennemy.GetComponent<Animator> ();
        otherAnimator.SetBool(DEAD_STATE, true);
        animator.SetBool(ATTACK_STATE, false);
        //Destroy(ennemy);
    } 

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");   

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        transform.Translate(movementDirection * speed * Time.deltaTime, Space.Self);

        if ((movementDirection != Vector3.zero) && (movementDirection != Vector3.forward) && (movementDirection != Vector3.back))
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        if (ennemy != null)
        {
            float distance = Vector3.Distance(ennemy.transform.position, transform.position);
            if (distance<1){
    //        MyAudioSource.Stop(); 
    //        MyAudioSource.clip = (AudioClip)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/sound/action-rock-109094.mp3", typeof(AudioClip));
    //        MyAudioSource.clip = Resources.Load<AudioClip>("Assets/sound/action-rock-109094.mp3");

            //Resources.Load<AudioClip>("Audioclips/" + filename);
    //        MyAudioSource.Play(); 

            Attack();
            }
        }
    }


/*
    void FixedUpdate(){
        transform.Translate(Vector3.forward * 5f * Time.fixedDeltaTime * Input.GetAxis("Vertical"));
        transform.Translate(Vector3.right * 5f * Time.fixedDeltaTime * Input.GetAxis("Horizontal"));
    }
*/  
}



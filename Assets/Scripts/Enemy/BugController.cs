using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BugController : MonoBehaviour
{

    public EnemyAttackScript enemyAttack; // Reference to attack script
    public float walkSpeed ;
    public float detectionRange ;
    public float ForgettingRange ;
    public float attackRange ;

    private enum EnemyState { Idle, Walking, Attacking }
    private EnemyState currentState = EnemyState.Idle;
    public Transform player;
    public Transform target {get;set;}
    private NavMeshAgent agent;

    private Rigidbody rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>(); // Finds Animator on Model
        agent = GetComponent<NavMeshAgent>();
        
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }


        rb.AddForce((player.position - transform.position).normalized * 2f, ForceMode.Acceleration);

    }
    

    void MoveTowardsPlayer()
    {
        agent.SetDestination(player.position);    
    }
    void FixedUpdate()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case EnemyState.Idle:
                if (distance <= detectionRange)
                {
                    currentState = EnemyState.Walking;
                    animator.SetBool("IsMoving", true);
                }
                break;

            case EnemyState.Walking:
                if (distance <= ForgettingRange)
                {
                   
                    MoveTowardsPlayer();
                    if (distance <= attackRange)
                    {
                        animator.SetBool("IsAttack", true);
                        currentState = EnemyState.Attacking;
                    }
                }
                else{
                    currentState = EnemyState.Idle;
                    animator.SetBool("IsAttack", false);
                    animator.SetBool("IsMoving", false);
                }
               
                break;
            case EnemyState.Attacking:

                if (distance <= attackRange)
                {
                    animator.SetBool("IsAttack", true);
                    enemyAttack.PerformAttack();
                    currentState = EnemyState.Walking;
                }else{
                    currentState = EnemyState.Idle;
                }

                break;

        }
    }


}

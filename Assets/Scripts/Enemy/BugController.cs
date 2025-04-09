using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BugController : MonoBehaviour
{

    private EnemyColliderManager colliderManager;
    private EnemyAttackScript enemyAttack; // Reference to attack script
    public float walkSpeed = 50f;
    public float detectionRange = 10f;
    public float attackRange = 2f;

    private enum EnemyState { Idle, Walking, Crushing, Crawling, Attacking }
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

        colliderManager = GetComponent<EnemyColliderManager>(); // Reference new script

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
                MoveTowardsPlayer();
                if (distance <= attackRange)
                {
                    // StartCrush();
                }
                break;

            case EnemyState.Crushing:
                // Crush handled in StartCrush()
                break;

            case EnemyState.Crawling:
                MoveTowardsPlayer();
                if (distance <= attackRange)
                {
                    currentState = EnemyState.Attacking;
                    animator.SetBool("IsAttacking", true);
                }
                else
                {
                    currentState = EnemyState.Crawling;
                    animator.SetBool("IsAttacking", false);
                }
                break;

            case EnemyState.Attacking:

                enemyAttack.PerformAttack();
                if (distance >= attackRange)
                {
                    animator.SetBool("IsAttacking", false);
                    currentState = EnemyState.Crawling;
                }

                break;

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (currentState == EnemyState.Crushing && collision.gameObject.CompareTag("Player"))
        {
            enemyAttack.CrushAttack(player.gameObject);


        }
    }

}

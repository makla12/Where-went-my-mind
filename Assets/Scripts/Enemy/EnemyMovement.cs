using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    private EnemyColliderManager colliderManager;
    private EnemyAttackScript enemyAttack; // Reference to attack script
    public float walkSpeed = 50f;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float crawlSpeed = 30f;
    public float crushSpeed = 15f; // Speed boost for the crash attack
    public float crushDuration = 8.2f; // How long the crush lasts
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
        enemyAttack = GetComponent<EnemyAttackScript>();

        rb.AddForce((player.position - transform.position).normalized * 2f, ForceMode.Acceleration);

    }

    void MoveTowardsPlayer(float speed)
    {
        
        agent.SetDestination(player.position);
        
        // Vector3 direction = (player.position - transform.position).normalized;   
        // Quaternion targetRotation = Quaternion.LookRotation(-new Vector3(direction.x, 0, direction.z));// Rotate the enemy to face the player
        // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);       
        // rb.AddForce(direction * speed, ForceMode.Acceleration);// Move towards the player using Rigidbody
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
                MoveTowardsPlayer(walkSpeed);
                if (distance <= attackRange)
                {
                    StartCrush();
                }
                break;

            case EnemyState.Crushing:
                // Crush handled in StartCrush()
                break;

            case EnemyState.Crawling:
                MoveTowardsPlayer(crawlSpeed);
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

    void StartCrush()
    {
        currentState = EnemyState.Crushing;
        animator.SetBool("IsCrushing", true);

        // Dash forward with high speed
        Vector3 crushDirection = (player.position - transform.position).normalized;
        rb.AddForce(crushDirection * crushSpeed, ForceMode.Impulse);


        colliderManager.ChangeCollider(crushDuration);

        StartCoroutine(WaitAndStartCrawling(crushDuration));

    }

    IEnumerator WaitAndStartCrawling(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for crushDuration seconds

        currentState = EnemyState.Crawling;
        animator.SetBool("IsCrushing", false);
        animator.SetBool("IsCrawling", true);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (currentState == EnemyState.Crushing && collision.gameObject.CompareTag("Player"))
        {
            enemyAttack.CrushAttack(player.gameObject);


        }
    }

}

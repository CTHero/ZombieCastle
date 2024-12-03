using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//walk/run animation is "state = 1"  attack is "state = 21"
public class EnemyAI : MonoBehaviour
{

    public LayerMask whatIsGround, whatIsPlayer;
    public Animator animatableCharacter;
    public Weapon theWeapon;

    //Patrolling variables
    Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange = 10f;
    //public bool drawGizmos = false;
    Vector3 lastPosition;
    int framesStuck = 0;

    //Attacking variables
    public float timeBetweenAttacks = 2f;
    bool alreadyAttacked;

    //States
    public float sightRange = 8f;
    public float attackRange = 5f;
    bool playerInSightRange, playerInAttackRange;

    NavMeshAgent agent;
    private Transform player;
    Animator animator;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //lastTransform = transform;
        if (animatableCharacter != null)
        {
            animator = animatableCharacter;
        }
        else
        {
            animator = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) patrolling();

        if (playerInSightRange && !playerInAttackRange) chasePlayer();

        if (playerInSightRange && playerInAttackRange) attackPlayer();

        //if (drawGizmos) onDrawGizmosSelected();

        if(lastPosition.x == transform.position.x && lastPosition.z == transform.position.z)
        {
            //Start counting to see if it's stuck
            framesStuck++;
            if(framesStuck > 30)
            {
                Debug.Log("Stuck!!!");
                searchWalkPoint();
            }
        }
        else
        {
            lastPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            framesStuck = 0;
        }
    }

    private void patrolling()
    {
        if(!walkPointSet) searchWalkPoint();
        if(walkPointSet) agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude < 2.0)  searchWalkPoint();

        if(animator != null) animator.SetInteger("state", 1);
    }

    private void searchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
            
        }
        else
        {
            Debug.Log("Point is NOT on ground.");
        }
    }
    private void chasePlayer()
    {
        //If we have found the player, then go after the player
        agent.SetDestination(player.position);
    }

    private void attackPlayer()
    {
        //Once you are in range to attack, stop moving toward the player
        agent.SetDestination(transform.position);

        //Make sure the agent is facing the player
        transform.LookAt(player.position);

        if (animator != null) animator.SetInteger("state", 20);  //Set to attack animation

        if (!alreadyAttacked)
        {
            // Attack code
            //Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity);
            //rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            //rb.AddForce(transform.forward * 8f, ForceMode.Impulse);

            if(theWeapon != null)
            {
                theWeapon.activate();
            }

            //Set a delay so that it doesn't attack every frame
            alreadyAttacked = true;
            Invoke(nameof(resetAttack), timeBetweenAttacks);
        }
    }

    private void resetAttack()
    {
        alreadyAttacked = false;
    }



    private void onDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, sightRange);
    }    
}

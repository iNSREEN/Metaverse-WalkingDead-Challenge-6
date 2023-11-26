using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Zombie1 : MonoBehaviour
{


    [Header("Zombie Health and Damage")]
/*    private float zombiHealth = 100f;
    private float presentHealth;*/
    public float giveDamage = 10f;
/*    public HealthBar healthBar;*/



    [Header("Zombie Things")]
    public NavMeshAgent zombieAgent;
    public Transform LookPoint;
    public Camera attaackingRayCastArea;
    public Transform playerBody;
    public LayerMask PlayerLayer;

    [Header("Zombie Gurding Varibles")]
    public GameObject[] walkpoints;
    int currentZombiePosition = 0;
    public float zombieSpeed;
    float walkingPointRadius = 2;


    [Header("Zombie Attacking VAraible")]
    public float timeBtwAttack;
    bool previousAttack;


/*
    [Header("Zombi Animation")]
    public Animator anim;*/


    [Header("Zombie Mood/States")]
    public float visionRadius;
    public float attackingRadius;
    public bool playerInVisionRadius;
    public bool playerInAttackingRaduis;


    private void Awake()
    {
        /*        presentHealth = zombiHealth;
                healthBar.GiveFullHealth(zombiHealth);*/
        zombieAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        playerInVisionRadius = Physics.CheckSphere(transform.position, visionRadius, PlayerLayer);
        playerInAttackingRaduis = Physics.CheckSphere(transform.position, attackingRadius, PlayerLayer);

        //if player not in attach or radios area keep garding the area 
        if (!playerInVisionRadius && !playerInAttackingRaduis) Guard();

        //if player in good vision area and not in the attacking radius follow player
        if (playerInVisionRadius && !playerInAttackingRaduis) PursuePlayer();

        // if player in vision area and in the attacking area then will attack
        if (playerInVisionRadius && playerInAttackingRaduis) AttackPlayer();
    }
    private void Guard()
    {
        if (Vector3.Distance(walkpoints[currentZombiePosition].transform.position, transform.position) < walkingPointRadius)
        {
            currentZombiePosition = Random.Range(0, walkpoints.Length);
            if (currentZombiePosition >= walkpoints.Length)
            {
                currentZombiePosition = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, walkpoints[currentZombiePosition].transform.position, Time.deltaTime * zombieSpeed);
        //change zombie facing
        transform.LookAt(walkpoints[currentZombiePosition].transform.position);

    }

    private void PursuePlayer()
    {

        zombieAgent.SetDestination(playerBody.position);
  /*      if (zombieAgent.SetDestination(playerBody.position))
        {
            //Animation
            anim.SetBool("Walking", false);
            anim.SetBool("Running", true);
            anim.SetBool("Attacking", false);
            anim.SetBool("Died", false);
        }
        else
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Running", false);
            anim.SetBool("Attacking", false);
            anim.SetBool("Died", true);
        }*/

    }

    private void AttackPlayer()
    {
        zombieAgent.SetDestination(transform.position); // stop zombu at one point at the attack radiuse 
        transform.LookAt(LookPoint); // make zomibi look at player 
                                     //check if the zombie didnt previusly attack
        if (!previousAttack)
        {
            RaycastHit hitInfo;
            //check if the recast move in the forword direction and revice information about object hit and restrict the ray by reduace
            if (Physics.Raycast(attaackingRayCastArea.transform.position, attaackingRayCastArea.transform.forward, out hitInfo, attackingRadius))
            {
                Debug.Log("attacking : " + hitInfo.transform.name);

                PlayerScript playerBody = hitInfo.transform.GetComponent<PlayerScript>();

                if (playerBody != null)
                {
                    playerBody.playerHitDamage(giveDamage);
                }

                /* anim.SetBool("Attacking", true);
                 anim.SetBool("Walking", false);
                 anim.SetBool("Running", true);
                 anim.SetBool("Died", false);*/

            }
            previousAttack = true;
            Invoke(nameof(ActiveAttacking), timeBtwAttack); //wait before attacking with timebteAttack secound
        }
    }
    private void ActiveAttacking()
    {

        previousAttack = false;

    }

    /* public void zombiHitDamage(float takeDamage)
     {
         presentHealth -= takeDamage;
         healthBar.SetHealth(presentHealth);

         if (presentHealth <= 0)
         {
             anim.SetBool("Walking", false);
             anim.SetBool("Running", false);
             anim.SetBool("Attacking", false);
             anim.SetBool("Died", true);

             zombiDie();
         }
     }
 */
    /*  private void zombiDie()
      {
          zombieAgent.SetDestination(transform.position);
          zombieSpeed = 0f;
          attackingRadius = 0f;
          visionRadius = 0f;
          playerInVisionRadius = false;
          playerInAttackingRaduis = false;
          Object.Destroy(gameObject, 5.0f); //damage after 5 secound
      }*/
}

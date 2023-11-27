using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    [Header("Zombie Health and Damage")]
    private float zombiHealth = 100f;
    private float presentHealth;
    public float giveDamage = 5f;

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

    [Header("Zombie Mood/States")]
    public float visionRadius;
    public float attackingRadius;
    public bool playerInVisionRadius;
    public bool playerInAttackingRaduis;

    [Header("Zombie Attacking VAraible")]
    public float timeBtwAttack;
    bool previouslyAttack;


    [Header("Zombi Animation")]
    public Animator anim;


    [Header("Rifle Audio ")]
    public AudioSource ZombieDamge;
  /*  public AudioSource ZombieWlak;*/
    public AudioSource ZombieRun;
    public AudioSource Zombiedie;

    private void Awake()
    {
        presentHealth = zombiHealth;
        zombieAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInVisionRadius = Physics.CheckSphere(transform.position, visionRadius, PlayerLayer);
        playerInAttackingRaduis = Physics.CheckSphere(transform.position, attackingRadius, PlayerLayer);

        if (!playerInVisionRadius && !playerInAttackingRaduis) Guard();
        if (playerInVisionRadius && !playerInAttackingRaduis)
        {
            ZombieRun.Play();
            PursuePlayer();
        }
        if (playerInVisionRadius && playerInAttackingRaduis) AttackPlayer();
    }
    // make pklayer wlak on deffrintspoints
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
        //zombie facing - look player
        transform.LookAt(walkpoints[currentZombiePosition].transform.position);

    }

    private void PursuePlayer()
    {
        if (zombieAgent.SetDestination(playerBody.position))
        {
            //Animation
       /*     ZombieRun.Play();*/
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
        }
    }

    private void AttackPlayer()
    {
        zombieAgent.SetDestination(transform.position);
        transform.LookAt(LookPoint);
        if (!previouslyAttack)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(attaackingRayCastArea.transform.position, attaackingRayCastArea.transform.forward, out hitInfo, attackingRadius))
            {
                Debug.Log("Attacking..." + hitInfo.transform.name);
                PlayerScript playerBody = hitInfo.transform.GetComponent<PlayerScript>();
                Debug.Log(playerBody);

                if (playerBody != null)
                {
                    playerBody.playerHitDamage(giveDamage);
                }


                anim.SetBool("Attacking", true);
                anim.SetBool("Walking", false);
                anim.SetBool("Running", false);
                anim.SetBool("Died", false);

            }
            previouslyAttack = true;
            Invoke(nameof(ActiveAttacking), timeBtwAttack);
        }

    }
    private void ActiveAttacking()
    {
        previouslyAttack = false;
    }


    public void zombiHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;
        if (presentHealth <= 0)
        {
            ZombieDamge.Play();
            anim.SetBool("Walking", false);
            anim.SetBool("Running", false);
            anim.SetBool("Attacking", false);
            anim.SetBool("Died", true);

            zombiDie();
        }

    }

    private void zombiDie()
    {
        
        zombieAgent.SetDestination(transform.position);
        zombieSpeed = 0f;
        attackingRadius = 0f;
        visionRadius = 0f;
        playerInVisionRadius = false;
        playerInAttackingRaduis = false;
        Zombiedie.Play();
        Object.Destroy(gameObject, 5.0f); //damage after 5 secound
    }


}

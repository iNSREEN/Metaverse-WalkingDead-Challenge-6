using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BossZombie : MonoBehaviour
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

    [Header("Zombie Standing Varibles")]

    public float zombieSpeed;
  

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

    /*  public AudioSource ZombieWlak;*/
    public AudioSource ZombieScreeem;
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

        if (!playerInVisionRadius && !playerInAttackingRaduis) Idle();
        if (playerInVisionRadius && !playerInAttackingRaduis) PursuePlayer();
        if (playerInVisionRadius && playerInAttackingRaduis) AttackPlayer();
    }
    // make pklayer wlak on deffrintspoints
    private void Idle()
    {
        zombieAgent.SetDestination(transform.position);
        anim.SetBool("Idle", true);
        anim.SetBool("Running", false);
    }

    private void PursuePlayer()
    {
        if (zombieAgent.SetDestination(playerBody.position))
        {
            ZombieScreeem.Play();
            anim.SetBool("Idle", false);
            anim.SetBool("Running", true);
            anim.SetBool("Attacking", false);
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

            }
            anim.SetBool("Attacking", true);
            anim.SetBool("Running", false);
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

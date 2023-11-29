using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public Animator anim;
    UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] Transform player;
    public LayerMask PlayerLayer;
    public Transform playerBody;

    [Header("Zombie mode")]
    public float visionRadius;
    public bool playerInVisionRadius;
  
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }


    void Update()
    {
        playerInVisionRadius = Physics.CheckSphere(transform.position, visionRadius, PlayerLayer);

        if (playerInVisionRadius)
        {

            agent.SetDestination(playerBody.position);
      

        }
        anim.SetBool("SeePlayer", playerInVisionRadius);


    }


}

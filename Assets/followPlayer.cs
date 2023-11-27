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

    [Header("Zombie Mood/States")]
    public float visionRadius;
    public bool playerInVisionRadius;
  
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInVisionRadius = Physics.CheckSphere(transform.position, visionRadius, PlayerLayer);
     
        if (playerInVisionRadius)
        {

            agent.SetDestination(playerBody.position);
            anim.SetBool("SeePlayer", true);
        }

        
    }


}

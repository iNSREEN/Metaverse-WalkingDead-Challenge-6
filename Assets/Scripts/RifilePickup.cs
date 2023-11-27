using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifilePickup : MonoBehaviour
{
    [Header("Rifile's")]
    public GameObject PlayerRifile;
    public GameObject PickupRifile;
    public PlayerPunch playerPunch;
/*    public GameObject rifleUI;*/

    [Header("Rifile Assign Things")]
    public PlayerScript player;
    private float redius = 2.5f;
    public Animator animator;
    private float nextTimeToPunch = 0f;
    public float punchCharge = 15f;

    [Header("Rifle Audio ")]

    /*  public AudioSource ZombieWlak;*/
    public AudioSource PickUp;
 


    private void Awake()
    {
        PlayerRifile.SetActive(false);
/*        rifleUI.SetActive(false);*/
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextTimeToPunch)
        {
            animator.SetBool("Punch", true);
            animator.SetBool("Idle", false);

            nextTimeToPunch = Time.time + 1f / punchCharge;

            playerPunch.Punch();
        }
        else
        {
            animator.SetBool("Punch", false);
            animator.SetBool("Idle", true);
        }


        if (Vector3.Distance(transform.position, player.transform.position) < redius)
        {
            if (Input.GetKeyDown("f"))

            {
                PickUp.Play();
                PlayerRifile.SetActive(true);
                PickupRifile.SetActive(false);
                //sound

                //objective Completed
            }
        }

    }
}

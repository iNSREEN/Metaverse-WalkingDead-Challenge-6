using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Rifle : MonoBehaviour
{
    [Header("Rifle Things")]
    public Camera cam;
    public float giveDamagOf = 5f;
    public float shootingRange = 100f;
    public float fireCharge = 15f; // max shooting before requier to recharge
    private float nextTimeToShoot = 0f;
    public PlayerScript player; //refrance to playerscript
    public Transform hand;
    public Animator animator;

    [Header("Rifle Ammiition and Shooting")]
    private int maxAmmunition = 32;
    public int mag = 10;
    private int presentAmmunition;
    public float reloadingTime = 1.3f;
    private bool setReloading = false;


    [Header("Rifle Effects")]
    public ParticleSystem muzzleSpark;
    public GameObject WoodedEffect;
 /*   public GameObject goreEffect; //blood effect*/


    private void Shoot()
    {
        if (mag == 0)
        {
            //show ammo out text
            return;
        }
        presentAmmunition--;
        if (presentAmmunition == 0)
        {
            mag--;
        }

        muzzleSpark.Play();
        RaycastHit hitInfo;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, shootingRange))
        {
            Debug.Log(hitInfo.transform.name);

            ObjectToHit objectToHit = hitInfo.transform.GetComponent<ObjectToHit>(); //from other script opjecttohit

            if (objectToHit != null)
            {
                objectToHit.ObjectHitDamage(giveDamagOf);
                GameObject WoodGo = Instantiate(WoodedEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));// it will show WoodGo effect whenever we recast
                Destroy(WoodGo, 1f);
            }
        }
    }

    private void Awake()
    {
        transform.SetParent(hand); //assign rifile parent to hand later will add multiple rifile
       /* RifleUI.SetActive(true); // show infromation UI about Ammo and Mag*/
        presentAmmunition = maxAmmunition;
    }

    void Start()
    {

    }

    void Update()
    {
        if (setReloading)
            return;

        if (presentAmmunition <= 0)
        {
            StartCoroutine(Reload());
            return;
        }


        if (Input.GetButton("Fire1") && Time.time >= nextTimeToShoot) //change from GetButtomdown to getbutton to shoot contenuace not single shot
        {
            animator.SetBool("Fire", true);
            animator.SetBool("Idle", false);
            nextTimeToShoot = Time.time + 1f / fireCharge;
            Shoot();
        }
        else if (Input.GetButton("Fire1") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            // if we are firing and also walking in same time set animation to FireWalk

            animator.SetBool("Idle", false);
            animator.SetBool("FireWalk", true);
        }
        else if (Input.GetButton("Fire2") && Input.GetButton("Fire1"))
        {
            //if we are firing and aiming

            animator.SetBool("Idle", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("FireWalk", true);
            animator.SetBool("Walk", true);
            animator.SetBool("Reloading", false);
        }
        else
        {
            animator.SetBool("Fire", false);
            animator.SetBool("Idle", true);
            animator.SetBool("FireWalk", false);
        }
    }


    //IEnumerator : as wait for secounds 
    IEnumerator Reload()
    {
        player.playerSpeed = 0f;
        player.playerSprint = 0f;
        setReloading = true;
        Debug.Log("Reloading...");

        //play reload animation
        animator.SetBool("Reloading", true);

        //play reload sound
        yield return new WaitForSeconds(reloadingTime);

        animator.SetBool("Reloading", false);
        presentAmmunition = maxAmmunition;
        player.playerSpeed = 1.9f;
        player.playerSprint = 3;
        setReloading = false;
    }
}


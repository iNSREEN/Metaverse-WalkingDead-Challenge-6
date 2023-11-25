using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [Header("Rifle Things")]
    public Camera cam;
    public float giveDamagOf = 5f;
    public float shootingRange = 100f;
    public float fireCharge = 15f; // max shooting before requier to recharge
    private float nextTimeToShoot = 0f;

    [Header("Rifle Effects")]
    public ParticleSystem muzzleSpark;
    public GameObject WoodedEffect;
 /*   public GameObject goreEffect; //blood effect*/


    private void Shoot()
    {
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

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToShoot) //change from GetButtomdown to getbutton to shoot contenuace not single shot
        {
            nextTimeToShoot = Time.time + 1f / fireCharge;
            Shoot();
        }
    }
}

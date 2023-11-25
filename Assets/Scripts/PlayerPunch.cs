using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    [Header("Player Punch Var")]
    public Camera cam;
    public float giveDamageOf = 10f;
    public float punchingRange = 5f;

    [Header("Punch Effects")]
    public GameObject WoodedEffect;
    public void Punch()
    {
        RaycastHit hitInfo; // store infromation about object we hit

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, punchingRange))
        {
            Debug.Log(hitInfo.transform.name);//show name iof whatever hit

            ObjectToHit objectToHit = hitInfo.transform.GetComponent<ObjectToHit>(); //from other script opjecttohit
            /*            Zombi1 zombi1 = hitInfo.transform.GetComponent<Zombi1>();
                        Zombi2 zombi2 = hitInfo.transform.GetComponent<Zombi2>();*/

            if (objectToHit != null) // shoot object 
            {
                objectToHit.ObjectHitDamage(giveDamageOf);
                GameObject WoodGo = Instantiate(WoodedEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(WoodGo, 1f);

            }
            /*            else if (zombi1 != null)
                        {
                            zombi1.zombiHitDamage(giveDamageOf);
                        }
                        else if (zombi2 != null)
                        {
                            zombi2.zombiHitDamage(giveDamageOf);
                        }*/

        }
    }
}
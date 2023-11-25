using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [Header("Camera to Assign")]
    public GameObject AimCam;
    public GameObject AimCanvas;

    public GameObject ThirdPersonCam;
    public GameObject ThirdPersonCanvas;


    [Header("Camera Animator")]
    public Animator animator;


    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()

    {
        if (Input.GetButton("Fire2") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) //Aiming position and walking
        {
            //if we are aiming and walking 

            animator.SetBool("Idle", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("RifleWalk", true);
            animator.SetBool("Walk", true);
            //------------

            ThirdPersonCam.SetActive(false);
            ThirdPersonCanvas.SetActive(false);

            AimCam.SetActive(true); //Activate Aiming Camera
            AimCanvas.SetActive(true);
        }
        else if (Input.GetButton("Fire2"))
        {
            //if we are Just Aiming 

            animator.SetBool("Idle", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("RifleWalk", false);
            animator.SetBool("Walk", false);
            //-------------------------------

            ThirdPersonCam.SetActive(false);
            ThirdPersonCanvas.SetActive(false);

            AimCam.SetActive(true); //Activate Aiming Camera
            AimCanvas.SetActive(true);
        }
        else
        {
            // not aiming

            animator.SetBool("Idle", true);
            animator.SetBool("IdleAim", false);
            animator.SetBool("RifleWalk", false);

            //---------------------

            ThirdPersonCam.SetActive(true);  //Activate Third person Camera(not shooting or aiming)
            ThirdPersonCanvas.SetActive(true);

            AimCam.SetActive(false);
            AimCanvas.SetActive(false);
        }
    }
}

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


/*    [Header("Camera Animator")]
    public Animator animator;*/


    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()

    {
        if (Input.GetButton("Fire2")) //Aiming position and walking
        {

            ThirdPersonCam.SetActive(false);
            ThirdPersonCanvas.SetActive(false);
            AimCam.SetActive(true);
            AimCanvas.SetActive(true);
        }
        else
        {

            ThirdPersonCam.SetActive(true);  //Activate Third person Camera(not shooting or aiming)
            ThirdPersonCanvas.SetActive(true);
            AimCam.SetActive(false);
            AimCanvas.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnHoverSound : MonoBehaviour
{
    public AudioSource myFx;

    public AudioClip hovFx;
    public AudioClip clickFx;
    /*  public Animator anim;*/


    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void HoverSound()
    {
/*
        anim.SetBool("isHOV", true);*/
        myFx.PlayOneShot(hovFx);
    }

    public void ClickSound()
    {
        myFx.PlayOneShot(clickFx);
    }

}

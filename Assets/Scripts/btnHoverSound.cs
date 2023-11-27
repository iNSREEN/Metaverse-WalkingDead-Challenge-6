using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnHoverSound : MonoBehaviour
{
    public AudioSource myFx;

    public AudioClip hovFx;
    public AudioClip clickFx;


    public void HoverSound()
    {
        myFx.PlayOneShot(hovFx);
    }

    public void ClickSound()
    {
        myFx.PlayOneShot(clickFx);
    }

}

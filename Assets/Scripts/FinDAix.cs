using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinDAix : MonoBehaviour
{

    public AudioSource collectAudio;


    void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.CompareTag("Aixi"))
        {
            collectAudio.Play();
            other.gameObject.SetActive(false);
       

        }

    }
}

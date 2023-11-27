using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinDAix : MonoBehaviour
{




    public AudioSource collectAudio;


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered!");
       
        if (other.gameObject.CompareTag("Aixi"))
        {
            Debug.Log("Aixi collided with: " + other.gameObject.name);
            Debug.Log("Aixi collided!");
            other.gameObject.SetActive(false);
            collectAudio.Play();

        }
    }
}

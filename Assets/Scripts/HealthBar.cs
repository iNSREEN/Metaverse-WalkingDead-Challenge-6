using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image healthBarImage;
    public AudioSource damgeAudio;
    private int Blood = 3;  // Assuming 3 is the maximum health
    public Animator animZombie;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            Debug.Log("Zombie Damge player");
            animZombie.SetBool("Attacing", true);
            damgeAudio.Play();
            Blood--;
            Debug.Log(Blood);

            // Update the fillAmount of the health bar image
            healthBarImage.fillAmount = (float)Blood / 3;

            if (Blood == 0)
            {
             
            }
        }
    }




}

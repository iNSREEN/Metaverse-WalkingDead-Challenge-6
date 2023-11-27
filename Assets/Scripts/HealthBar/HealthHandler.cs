using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHandler : MonoBehaviour
{
    public Slider slider;
    public float valu;


    HealthSystem healthSystem = new HealthSystem(100);


    public void Update()
    {
        slider.value = healthSystem.gethealthpercentage();
        valu = healthSystem.gethealthpercentage();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Zombie")
        {
            healthSystem.Damage(5);
        }
        if (collision.gameObject.tag == "Heal")
        {
            healthSystem.heal(5);
        }

    }

}

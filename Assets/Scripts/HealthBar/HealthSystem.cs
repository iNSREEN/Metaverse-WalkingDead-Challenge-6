using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class HealthSystem
{
    public float healthPoints;
    public float maxHealth;


    public HealthSystem(int maxHealth)
    {
        this.maxHealth = maxHealth;
        healthPoints = maxHealth;
    }

    public float gethealth() { return healthPoints; }
    public float gethealthpercentage() { return healthPoints / maxHealth; }

    public void Damage(int damageAmount)
    {
        healthPoints -= damageAmount;
        if (healthPoints <= 0) { healthPoints = 0; }
    }

    public void heal(int healAmount)
    {
        healthPoints += healAmount;
        if (healthPoints > maxHealth) { healthPoints = maxHealth; }
    }
}

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Constitution : MonoBehaviour
{
    private int healthPoints;
    private int maxHealth;
    private bool isDead;


    public Constitution(int hp) 
    {
        this.maxHealth = hp;
    }

    private void Awake()
    {
        this.healthPoints = maxHealth;
    }

    public void decreaseHealthPoints(int hpReduction)
    {
        this.healthPoints -= hpReduction;
        if (this.healthPoints <= 0 )
        {
            isDead = true;
        }
    }

    public int getHealthPoints()
    {
        return this.healthPoints;
    }

}
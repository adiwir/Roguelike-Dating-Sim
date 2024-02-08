using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity

{
    public int startingAbilityAmount;
    public List <Ability> abilities;
    BasicAbility basicAbility;
    
    public bool hasActiveAbilityLeft;

    //public BasicAbility basicAbility = //TODO: Lägg basicAbility här;
    //

    public void Awake()
    {
        isFriendly = true;
        isDead = false;
        moveDistance = 1;
        this.constitution = gameObject.AddComponent<Constitution>();
        //this.basicAbility = gameObject.AddComponent<PlayerBasicAbility>();

        assignStartingAbilities();
    }

    public void assignStartingAbilities()
    {

    }

    public void Start()
    {
        
    }

    public void Update()
    {

    }

    private void useVariableAbility()
    {

    }

    private void useBasicAbility()
    {

    }


    //getters and setters

    public int getCurrentHP()
    {
        return this.constitution.getHealthPoints();
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity

{
    //private int startingAbilityAmount;
    public List<Ability> abilities;
    BasicAbility basicAbility;

    public bool hasActiveAbilityLeft;

    //public BasicAbility basicAbility = //TODO: Lägg basicAbility här;
    //
    enum Orientation
    {
        north,
        south,
        west,
        east
    }
    Orientation orientation = Orientation.south;

    public void Awake()
    {
        //isFriendly = true;
        //isDead = false;
        moveDistance = 1;
        //this.constitution = gameObject.AddComponent<Constitution>();
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

    //public int getCurrentHP()
    //{
    //    return this.constitution.getHealthPoints();
    //}

    public void SetOrientation(string key)
    {
        switch (key)
        {
            case "W":
                orientation = Orientation.north;
                break;

            case "S":
                orientation = Orientation.south;
                break;

            case "A":
                orientation = Orientation.west;
                break;

            case "D":
                orientation = Orientation.east;
                break;

            default:
                //do nothing
                break;
        }
    }

    public string getOrientationAsString()
    {
        return orientation.ToString();
    }

}
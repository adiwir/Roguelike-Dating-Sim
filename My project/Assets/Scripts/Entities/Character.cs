using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity

{
    private readonly int startingAbilityAmount = 3;
    public Queue<ActiveAbility> abilities;
    public Queue<string> activeAbilities { get; set; }
    public List<string> assignedAbilities { get; set; }
    BasicAbility basicAbility;

    private int healthPoints;
    private int maxHealth = 4;
    private bool isDead;
    public bool hasActiveAbilityLeft = true;


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

    public void Start()
    {
        this.healthPoints = maxHealth;
    }

    public void Awake()
    {
        //isFriendly = true;
        //isDead = false;
        moveDistance = 1;
        //this.basicAbility = gameObject.AddComponent<PlayerBasicAbility>();
        
        EnqueueStartingAbilities();
        AssignAbilities();
    }

    //public void EnqueueStartingAbilities()
    //{ //Vad vi ska ha
    //    abilities = new Queue<ActiveAbility>();
    //    abilities.Enqueue(new Fireball());
    //    abilities.Enqueue(new Fireball());
    //    abilities.Enqueue(new Shield());
    //}

    public void decreaseHealthPoints(int hpReduction)
    {
        this.healthPoints -= hpReduction;
        if (this.healthPoints <= 0)
        {
            isDead = true;
        }
    }

    public void EnqueueStartingAbilities()
    {
        activeAbilities = new Queue<string>();
        activeAbilities.Enqueue("C4");
        activeAbilities.Enqueue("C4");
        activeAbilities.Enqueue("Forcefield");
        activeAbilities.Enqueue("C4");
        activeAbilities.Enqueue("Shove");
        activeAbilities.Enqueue("Shoot Laser");
    }

    void AssignAbilities()
    {
        assignedAbilities = new List<string>(2);
        for(int i = 0; i < startingAbilityAmount; i++)
        {
            assignedAbilities[i] = activeAbilities.Dequeue();
        }
    }

    public string GetAndDequeueAbility(int spot)
    {
        string activatedAbility = assignedAbilities[spot];
        if (activeAbilities.Count > 0)
        {
            assignedAbilities[spot] = activeAbilities.Dequeue();
        } else
        {
            hasActiveAbilityLeft = false;
            assignedAbilities[spot] = null; //inte så bra att den kan returnera null men får fixa det vid ett senare tilfälle.
            //borde också säga till den att sätta ett kryss på abilityns plats här i HUD:en och att knappen/platsen stängs av
        }
        
        return activatedAbility;
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            decreaseHealthPoints(1);
            Debug.Log("Player health is: " + healthPoints);
        }
    }

    private void useVariableAbility()
    {

    }

    private void useBasicAbility()
    {

    }

    //getters and setters



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

    public int getHealthPoints()
    {
        return this.healthPoints;
    }
}
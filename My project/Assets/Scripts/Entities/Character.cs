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

    public bool hasActiveAbilityLeft = true;

    //public BasicAbility basicAbility = //TODO: L�gg basicAbility h�r;
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
            Debug.Log(i);
            //assignedAbilities[i] = activeAbilities.Dequeue();
            assignedAbilities.Add(activeAbilities.Dequeue());
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
            assignedAbilities[spot] = null; //inte s� bra att den kan returnera null men f�r fixa det vid ett senare tilf�lle.
            //borde ocks� s�ga till den att s�tta ett kryss p� abilityns plats h�r i HUD:en och att knappen/platsen st�ngs av
        }
        
        return activatedAbility;
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
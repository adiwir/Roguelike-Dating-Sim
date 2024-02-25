using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Character : Entity
{
    private readonly int startingAbilityAmount = 3;
    public Queue<ActiveAbility> abilityQueue;
    public List<ActiveAbility> assignedAbilities { get; set; }
    [SerializeField] private BasicAbility basicAbility;
    private Vector3 pos;
    private AbilityManager abilityManager;

    private int healthPoints;
    private int maxHealth = 4;
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

    public void Start()
    {
        this.healthPoints = maxHealth;
        abilityQueue = abilityManager.GetAbilityQueue();
        Debug.Log("ability amount " + abilityQueue.Count);
        AssignAbilities();
    }

    public void Awake()
    {
        moveDistance = 1;
        abilityManager = GetComponent<AbilityManager>();
        
        

        //abilityQueue = AbilityManager.Instance.GetAbilityQueue();
    }

    void AssignAbilities()
    {
        assignedAbilities = new List<ActiveAbility>(3);
        for (int i = 0; i < startingAbilityAmount; i++)
        { 
            assignedAbilities.Add(abilityQueue.Dequeue());
        }
    }

    //public ActiveAbility GetAndDequeueAbility(int spot)
    //{
    //    ActiveAbility activatedAbility = assignedAbilities[spot];
    //    assignedAbilities[spot] = abilityQueue.Dequeue();
    //    if(abilityQueue.Count <= 0)
    //    {
    //        hasActiveAbilityLeft = false;
    //        assignedAbilities[spot] = null;
    //    }
        
    //    return activatedAbility;
    //}

    private void useVariableAbility()
    {

    }

    public void UseBasicAbility(Vector3Int cellToAttack)
    {
        Vector3Int addVector = new();
        switch (orientation)
        {
            case Orientation.north:
                addVector.x = 1;
                break;
            case Orientation.south:
                cellToAttack.x = -1;
                break;
            case Orientation.west:
                cellToAttack.y = 1;
                break;
            case Orientation.east:
                cellToAttack.y = -1;
                break;
        }
        AttackNextCell(cellToAttack, addVector);

    }

    private void AttackNextCell(Vector3Int closestTargetCell, Vector3Int addVec) //TODO: Loopa denna(för basic loopa 2 gånger).
    {
        Enemy enemy;
        
        for(int i = 1; i <= basicAbility.GetRange(); i++)
        {
            
            enemy = EnemyPosStorage.Instance.GetEnemyOnCell(closestTargetCell + (addVec*i));
            if (enemy != null)
            {
                Debug.Log("hitting enemy");
                enemy.TakeDamage(basicAbility.GetDamage());
                break;
            }
        }
    }

    public void ActivateAbilityInSpot(int spot)
    {
        if (ActivesAvailable())
        {
            //assignedAbilities[spot].UseAbility(this,);
            //Debug.Log(assignedAbilities[spot]);
            if (assignedAbilities[spot] != null) 
            {
                assignedAbilities[spot].CanIActivate(); 
            } else
            {
                print("That button doesn't have an ability"); //TODO: fixa så att man inte kan aktivera den alls om den är tom
            }
        }
        assignedAbilities[spot] = null;
        if (abilityQueue.Count <= 0)
        {
            
            hasActiveAbilityLeft = false;
            //TODO: visa på HUD att abilityQueue är använda
        }
        else { assignedAbilities[spot] = abilityQueue.Dequeue(); }

    }

    private bool ActivesAvailable()
    {
        bool hasAssignedAbility = false;
        foreach(ActiveAbility active in assignedAbilities)
        {
            if (active != null) { hasAssignedAbility = true; }
        }
        return hasAssignedAbility;
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

    public string GetOrientationAsString()
    {
        return orientation.ToString();
    }

    public Vector3 GetPos()
    {
        return this.pos;
    }

    public void SetPos(Vector3 vector)
    {
        this.pos = vector;
    }

    public float GetMoveSpeed()
    {
        return this.moveDistance;
    }

    public int getHealthPoints()
    {
        return this.healthPoints;
    }
}
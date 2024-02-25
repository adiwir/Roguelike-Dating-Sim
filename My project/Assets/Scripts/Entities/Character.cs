using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.Tilemaps;

public class Character : Entity
{
    private readonly int startingAbilityAmount = 3;
    public Queue<ActiveAbility> abilityQueue;
    public List<ActiveAbility> assignedAbilities { get; set; }
    [SerializeField] private BasicAbility basicAbility;
    ActiveAbility toggledAbility;
    public List<Vector3Int> areaOfEffect;

    private Vector3 pos;
    private AbilityManager abilityManager;

    private int healthPoints;
    private int maxHealth = 4;
    public bool hasActiveAbilityLeft = true;

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
    }

    void AssignAbilities()
    {
        assignedAbilities = new List<ActiveAbility>(3);
        for (int i = 0; i < startingAbilityAmount; i++)
        { 
            assignedAbilities.Add(abilityQueue.Dequeue());
        }
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
        if(toggledAbility != null && ReferenceEquals(toggledAbility, assignedAbilities[spot]))
        {
            ActivateToggledAbility(spot);
        }
        else
        {
            ToggleAbilityInSpot(spot);
        }
    }

    private void ActivateToggledAbility(int spot)
    {
        print("ActivatedAbility");
        toggledAbility.CanIActivate();
        toggledAbility = null;

        if (abilityQueue.Count <= 0)
        {
            hasActiveAbilityLeft = false;
            //TODO: visa på HUD att abilityQueue är använda
        }
        else { assignedAbilities[spot] = abilityQueue.Dequeue(); }
    }

    public void ToggleAbilityInSpot(int spot)
    {
        if (ActivesAvailable())
        {
            //assignedAbilities[spot].UseAbility(this,);
            //Debug.Log(assignedAbilities[spot]);
            if (assignedAbilities[spot] != null) 
            {
                print("toggledAbility");
                toggledAbility = assignedAbilities[spot];
                areaOfEffect = toggledAbility.GetAreaOfEffect();
                DisplayAreaOfEffect();
            } else
            {
                print("That button doesn't have an ability"); //TODO: fixa så att man inte kan aktivera den alls om den är tom
            }
        }
    }

    private void DisplayAreaOfEffect()
    {
        Vector3 mousePos = Input.mousePosition;
        //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //TODO: Adi hjälp här pls :).
        //Vector3Int mouseTargetCell = tilemap.WorldToCell(mousePos);
        
        foreach (Vector3Int tile in areaOfEffect)
        {
            //tile += mouseTargetCell;
        }
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
   
   public bool UsedAbility()
   {
        return (this.toggledAbility == null);
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
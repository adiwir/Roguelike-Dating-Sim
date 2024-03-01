using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.TextCore.Text;
using UnityEngine.Tilemaps;

public class Character : Entity
{
    [SerializeField] private Tilemap tilemap;
    private readonly int startingAbilityAmount = 3;
    public Queue<ActiveAbility> abilityQueue = new();
    public List<ActiveAbility> assignedAbilities { get; set; }
    [SerializeField] private BasicAbility basicAbility;
    ActiveAbility toggledAbility;
    public List<Vector3Int> areaOfEffect;
    List<Vector2Int> twoDAreaOfEffect = new List<Vector2Int>();
    List<Vector3Int> newAreaOfEffect;


    private Vector3 pos;
    private AbilityManager abilityManager;

    private int healthPoints;
    private int maxHealth = 4;
    public bool hasActiveAbilityLeft = true;

    public ImageChooser imageChooser;

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
        CombineAbilityQueues(abilityManager.GetAbilityQueue());
        Debug.Log("ability amount " + abilityQueue.Count);
        AssignAbilities();
    }

    public void Awake()
    {
        moveDistance = 1;
        abilityManager = GetComponent<AbilityManager>();
    }

    private void AssignAbilities()
    {
        assignedAbilities = new List<ActiveAbility>(3);
        for (int i = 0; i < startingAbilityAmount; i++)
        { 
            assignedAbilities.Add(abilityQueue.Dequeue());
        }
    }

    private void CombineAbilityQueues(Queue<ActiveAbility> queueToAdd) 
    {
        while (queueToAdd.Count > 0)
        {
            abilityQueue.Enqueue(queueToAdd.Dequeue());
        }
    }

    public void UseBasicAbility(Vector3Int cellToAttack)
    {
        Vector3Int addVector = new Vector3Int(0,0,0);
        switch (orientation)
        {
            case Orientation.north:
                addVector.x = 1;
                cellToAttack.x += 1;
                break;
            case Orientation.south:
                addVector.x = -1;
                cellToAttack.x += -1;
                break;
            case Orientation.west:
                addVector.y = 1;
                cellToAttack.y += 1;
                break;
            case Orientation.east:
                addVector.y = -1;
                cellToAttack.y += -1;
                break;
        }
        AttackNextCell(cellToAttack, addVector);

    }

    private void AttackNextCell(Vector3Int closestTargetCell, Vector3Int addVec) //TODO: Loopa denna(för basic loopa 2 gånger).
    {
        Enemy enemy;
        
        for(int i = 0; i <= basicAbility.GetRange()-1; i++)
        {
            //Debug.Log(i);
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
            Debug.Log(toggledAbility.ToString());
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
        //CalculateTargetArea();
        //toggledAbility.CanIActivate();
        if(toggledAbility.isAttackAbility)
        {
            AttackEnemiesInArea();
        }
        else 
        {
            useBuffAbility();
        }
        
        toggledAbility = null;

        HideAOE();

        abilityManager.SendActiveToDiscard(assignedAbilities[spot]);

        if (abilityQueue.Count <= 0)
        {
            hasActiveAbilityLeft = false;
            //imageChooser.SetOutOfAbilities(spot);
            //TODO: visa på HUD att abilityQueue är använda
        }
        else { 
            assignedAbilities[spot] = abilityQueue.Dequeue();
            //imageChooser.ImageChange(spot,assignedAbilities(spot).getIcon();
            imageChooser.ImageChange(spot,);
        }
    }

    private void useBuffAbility()
    {
        toggledAbility.UseAbility(this);
    }

    private void AttackEnemiesInArea()
    {
        List<Vector3Int> tilesToAttack = CalculateTargetArea();
        toggledAbility.UseAbility(tilesToAttack);
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
                
                //imageChooser.toggleImage(spot);
            } else
            {
                print("That button doesn't have an ability"); //TODO: fixa så att man inte kan aktivera den alls om den är tom
            }
        }
    }

    private List<Vector3Int> CalculateTargetArea()//kanske borde returna egentligen men då måste metoden ändras
    {
        Vector3 mousePos = MousePos.Instance.GetHoveredNode();
        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        Vector3Int mouseTargetCell = tilemap.WorldToCell(mousePos);
        List<Vector3Int> tilesToTarget = new List<Vector3Int>();

        newAreaOfEffect = new List<Vector3Int>();

        foreach (Vector3Int tile in areaOfEffect)
        {
            //twoDAreaOfEffect.Add(((tile + mouseTargetCell).Vector2Int));
            twoDAreaOfEffect.Add(new Vector2Int(tile.x + mouseTargetCell.x, tile.y + mouseTargetCell.y));
            tilesToTarget.Add(tile+mouseTargetCell);
            newAreaOfEffect.Add(tile);
        }
        return tilesToTarget;
    }

    public void DisplayAreaOfEffect()
    {
        HideAOE();

        if (toggledAbility.affectsAnArea)
        {
            
            CalculateTargetArea();

            foreach (Vector2Int node in twoDAreaOfEffect)
            {
                if (finished2.MapManager.Instance.map.ContainsKey(node))
                {
                    finished2.MapManager.Instance.map[node].ShowTile();
                }
            }
    
            areaOfEffect.Clear();
            areaOfEffect = newAreaOfEffect;
        }
    }

    private void HideAOE()
    {
        foreach (Vector2Int node in twoDAreaOfEffect)
        {
            if (finished2.MapManager.Instance.map.ContainsKey(node))
            {
                finished2.MapManager.Instance.map[node].HideTile();
            }
        }

        twoDAreaOfEffect.Clear();
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

    public void RechargeAbilities()
    {
        if (abilityManager.HasADiscardedAbility)
        {
            CombineAbilityQueues(abilityManager.Recharge());
            AssignAbilities();
        }
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

    public bool UsedAbility() //kan vara buggig
   {
        return (this.toggledAbility == null);
   }

    public void UnToggleAbility()
    {
        toggledAbility = null;
        HideAOE();
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

    public ActiveAbility GetToggledAbility()
    {
        return this.toggledAbility;
    }
}
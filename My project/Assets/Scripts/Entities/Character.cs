using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using UnityEditor.UI;
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
    List<Vector2Int> twoDAreaOfEffect = new();
    List<Vector3Int> newAreaOfEffect;

    private Vector3 pos;
    private AbilityManager abilityManager;

    private int healthPoints;
    private int maxHealth = 4;

    [SerializeField] private ImageChooser imageChooser;

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
        moveDistance = 1;
        abilityManager = GetComponent<AbilityManager>();
    }

    public void Start()
    {
        this.healthPoints = maxHealth;
        abilityQueue = CombineAbilityQueues(abilityManager.GetAbilityQueue());
        Debug.Log("ability amount " + abilityQueue.Count);
        assignedAbilities = new List<ActiveAbility>();
        AssignAbilities(abilityQueue);
    }

    private void AssignAbilities(Queue<ActiveAbility> queue)
    {
        for (int i = 0; i < startingAbilityAmount; i++)
        { 
            assignedAbilities.Add(queue.Dequeue());
            imageChooser.ImageChange(i, assignedAbilities[i].GetName());
        }
        imageChooser.ImageChange(-1, queue.Peek().GetName());
    }

    private Queue<ActiveAbility> CombineAbilityQueues(Queue<ActiveAbility> queueToAdd) 
    {
        while (queueToAdd.Count > 0)
        {
            abilityQueue.Enqueue(queueToAdd.Dequeue());
        }
        return abilityQueue;
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
    private void AttackNextCell(Vector3Int closestTargetCell, Vector3Int addVec)
    {
        List<Vector3Int> targetCells = new List<Vector3Int>();

        for (int i = 0; i <= basicAbility.GetRange()-1; i++)
        {
            targetCells.Add(closestTargetCell + addVec * i);
        }

        HashSet<Enemy> hitEnemies = EnemyPosStorage.Instance.GetEnemyOnCell(targetCells);
        if (hitEnemies != null)
        {
            foreach (Enemy enemy in hitEnemies)
            {
                enemy.TakeDamage(basicAbility.GetDamage());
            }
        }
    }

    public void ActivateAbilityInSpot(int spot)
    {
        
        if (toggledAbility != null && ReferenceEquals(toggledAbility, assignedAbilities[spot]))
        {
            ActivateToggledAbility(spot);
        }
        else
        {
            if (!ReferenceEquals(toggledAbility, assignedAbilities[spot]))
            {
                print("reference doesn't equal");
            }
            ToggleAbilityInSpot(spot);

        }
    }

    private void ActivateToggledAbility(int spot)
    {

        bool succesfullyUsedAbility = false;
        if(toggledAbility.isAttackAbility)
        {
            succesfullyUsedAbility = AttackEnemiesInArea();
        }
        else 
        {
            useBuffAbility();
        }

        if (succesfullyUsedAbility || !toggledAbility.isAttackAbility) 
        {
            
            imageChooser.AddLastAbilityIconToDiscard(toggledAbility.GetName());
            imageChooser.ToggleBorder(spot);
            toggledAbility = null;

            HideAOE();

            abilityManager.SendActiveToDiscard(assignedAbilities[spot]);

            if (abilityQueue.Count <= 0)
            {
                assignedAbilities[spot] = null;
                imageChooser.SetOutOfAbilities(spot);
            }
            else
            {
                assignedAbilities[spot] = abilityQueue.Dequeue();

                imageChooser.ImageChange(spot, assignedAbilities[spot].GetName());
                if (abilityQueue.Count > 0)
                {
                    imageChooser.ImageChange(-1, abilityQueue.Peek().GetName());
                }
                else
                {
                    imageChooser.SetOutOfAbilities(-1);
                }
            }
        }
    }

    private void useBuffAbility()
    {
        toggledAbility.UseAbility(this);
    }

    private bool AttackEnemiesInArea()
    {
        Vector3 mousePos = MousePos.Instance.GetHoveredNode();
        Vector3Int mouseTargetCell = tilemap.WorldToCell(mousePos);
        Vector3Int characterCell = tilemap.WorldToCell(this.pos);
        Vector2Int twoDCharacterCell = new(characterCell.x, characterCell.y);

        List<Vector3Int> charactersTileList = new List<Vector3Int>();
        charactersTileList.Add(characterCell);

        List<Vector3Int> tilesToAttack = CalculateTargetArea(mouseTargetCell);
        HashSet<Vector2Int> areaInRange = AreaInRange.CalcAreaInRange(toggledAbility.range, twoDCharacterCell);
        if(toggledAbility.GetName() == "Stomp")
        {
            tilesToAttack = CalculateTargetArea(characterCell);
            toggledAbility.UseAbility(tilesToAttack);
            return true;
        }
        else if (areaInRange.Contains(new Vector2Int(mouseTargetCell.x, mouseTargetCell.y)))
        {
            toggledAbility.UseAbility(tilesToAttack);
            return true;
        }
        else
        {
            print("Not in range!");
            return false;
        }
    }

    public void ToggleAbilityInSpot(int spot)
    {
        if (ActivesAvailable(assignedAbilities))
        {
            print(assignedAbilities[spot]);
            if (assignedAbilities[spot] != null) 
            {
                imageChooser.ToggleBorder(spot);
                toggledAbility = assignedAbilities[spot];
                areaOfEffect = toggledAbility.GetAreaOfEffect();
                DisplayAreaOfEffect();
                
            } else
            {
                imageChooser.SetOutOfAbilities(spot);
                print("That button doesn't have an ability");
            }
        } //TODO: säg åt spelaren att de kan ladda om med R
    }

    private List<Vector3Int> CalculateTargetArea(Vector3Int mouseTargetCell)
    {

        List<Vector3Int> tilesToTarget = new List<Vector3Int>();
        newAreaOfEffect = new List<Vector3Int>();

        foreach (Vector3Int tile in areaOfEffect)
        {
            
            tilesToTarget.Add(tile+mouseTargetCell);
            twoDAreaOfEffect.Add(new Vector2Int(tile.x + mouseTargetCell.x , tile.y + mouseTargetCell.y ));
            newAreaOfEffect.Add(tile);
        }
        return tilesToTarget;
    }

    public void DisplayAreaOfEffect()
    {
        HideAOE();

        if (toggledAbility.affectsAnArea)
        {
            Vector3 mousePos = MousePos.Instance.GetHoveredNode();
            Vector3Int mouseTargetCell = tilemap.WorldToCell(mousePos);
            Vector3Int characterCell = tilemap.WorldToCell(this.pos);
            Vector2Int twoDCharacterCell = new(characterCell.x, characterCell.y);
            
            HashSet<Vector2Int> areaInRange = AreaInRange.CalcAreaInRange(toggledAbility.range, twoDCharacterCell);
            if (toggledAbility.GetName() == "Stomp")
            {
                CalculateTargetArea(characterCell);

                foreach (Vector2Int node in twoDAreaOfEffect)
                {
                    if (finished2.MapManager.Instance.map.ContainsKey(node))
                    {
                        finished2.MapManager.Instance.map[node].ShowTile();
                    }
                }
                areaOfEffect = newAreaOfEffect;
            }
            else if (areaInRange.Contains(new Vector2Int(mouseTargetCell.x, mouseTargetCell.y)))
            {
                CalculateTargetArea(mouseTargetCell);

                foreach (Vector2Int node in twoDAreaOfEffect)
                {
                    if (finished2.MapManager.Instance.map.ContainsKey(node))
                    {
                        finished2.MapManager.Instance.map[node].ShowTile();
                    }
                }
                areaOfEffect = newAreaOfEffect;
            }
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

    private bool ActivesAvailable(List<ActiveAbility> list)
    {
        bool hasAssignedAbility = false;
        foreach(ActiveAbility active in list)
        {
            if (active != null) { hasAssignedAbility = true; }
        }
        return hasAssignedAbility;
    }

    public void RechargeAbilities()
    {
        if (abilityManager.HasADiscardedAbility)
        {
            abilityQueue.Clear();
            assignedAbilities.Clear();
            abilityQueue = CombineAbilityQueues(abilityManager.Recharge());
            AssignAbilities(abilityQueue);
            imageChooser.AddLastAbilityIconToDiscard("Transparent");
            imageChooser.UntoggleAllBorders();
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

    public bool CheckIfAllAbilitiesUsed()
    {
        Debug.Log("checking if abilities used");
        bool noAssignedAbilities = assignedAbilities.All(item => item == null);
        Debug.Log(noAssignedAbilities);
        return (noAssignedAbilities && abilityQueue.Count <= 0);
    }

    public bool UsedAbility()
    {
        return (this.toggledAbility == null);
    }

    public void UnToggleAbility()
    {
        imageChooser.UntoggleAllBorders();
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
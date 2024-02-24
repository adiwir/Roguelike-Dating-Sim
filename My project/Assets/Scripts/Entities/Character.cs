using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Character : Entity

{
    private readonly int startingAbilityAmount = 3;
    public Queue<ActiveAbility> abilities;
    public Queue<string> activeAbilities { get; set; }
    public List<string> assignedAbilities { get; set; }
    [SerializeField] private BasicAbility basicAbility;
    private Vector3 pos;

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
    }

    public void Awake()
    {
        moveDistance = 1;
        
        EnqueueStartingAbilities();
        AssignAbilities();
    }

    public void EnqueueStartingAbilities()
    {
        activeAbilities = new Queue<string>();
        activeAbilities.Enqueue("StickyBomb");
        activeAbilities.Enqueue("StickyBomb");
        activeAbilities.Enqueue("Forcefield");
        activeAbilities.Enqueue("StickyBomb");
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
using BarthaSzabolcs.Tutorial_SpriteFlash;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Sheep : Enemy
{
    private EnemyController controller;
    public int timeThresh;
    private int elapsedTime = 0;
    private bool isAttacking;
    private bool isFlipped;
    public int range;
    public List<Vector2Int> attackTiles;
    EnemySubject enemySubject;
    public GameObject playerTarget;
    public Animator animator;

    public void Awake()
    {
        controller = GetComponent<EnemyController>();
        this.maxHp = 3;
        this.hp = maxHp;
        this.pos = controller.GetPos();
        //pos = tilemap.WorldToCell(transform.position);
        if (TryGetComponent<EnemySubject>(out enemySubject))
        {
            // Register as an observer
            enemySubject.RegisterObserver(this);
        }
        else
        {
            Debug.LogError("EnemySubject not found.");
        }
        UpdateEnemyPosition(this.pos);
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        healthBar.UpdateHealthBar(hp, maxHp);
        isAttacking = false;
        playerTarget = GameObject.FindWithTag("Player");
        damageFlash = GetComponent<DamageFlash>();
    }

    private void Update()
    {
        Vector3 scale = transform.localScale;

        if (playerTarget.transform.position.y > transform.position.y)
        {
            isFlipped = true;

        }
        else
        {
            isFlipped = false;
        }

        if (playerTarget.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (isFlipped ? -1 : 1);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * (isFlipped ? -1 : 1);
        }

        animator.SetBool("isFlipped", isFlipped);
        transform.localScale = scale;

    }
    void LateUpdate()
    {

        if (CheckRange() && !isAttacking)
        {
            attackTiles = GetAttackRange();
            controller.ShowAttack(attackTiles);
            isAttacking = true;
            elapsedTime = 0;
        }
        else if (isAttacking)
        {
            animator.SetBool("isRunning", false);
            controller.ShowAttack(attackTiles);
            if (elapsedTime > timeThresh)
            {
                FireBlast();
                elapsedTime = 0;
                controller.HideAttack(attackTiles);
                attackTiles.Clear();
                isAttacking = false;
            }
        }
        else if (!isAttacking && elapsedTime > timeThresh)
        {
            controller.MoveAlongPath();
            animator.SetBool("isRunning", true);
            elapsedTime = 0;
        }

        /*
        if (elapsedTime > 90)
        {
            Debug.Log(CheckRange());
            elapsedTime = 0;
        }*/
        this.pos=controller.GetPos();

        elapsedTime += 1;
    }


    private void FireBlast()
    {
        
        foreach (Vector2Int tilePos in attackTiles)
        {
            if (finished2.MapManager.Instance.map[tilePos] == controller.target.standingOnTile)
            {
                controller.target.GetComponentInParent<Health>().decreaseHealthPoints(1);
                return;
            }
        }
    }

    private bool CheckRange()
    {
        for (int y = range * (-1); y <= range; y++)
        {
            for (int x = range * (-1); x <= range; x++)
            {
                if (finished2.MapManager.Instance.map.ContainsKey(new Vector2Int(controller.enemyTile.gridLocation.x + x, controller.enemyTile.gridLocation.y + y))
                    && Mathf.Abs(x) + Mathf.Abs(y) <= range
                    && finished2.MapManager.Instance.map[new Vector2Int(controller.enemyTile.gridLocation.x + x, controller.enemyTile.gridLocation.y + y)] == controller.target.standingOnTile)
                {
                    return true;
                }
            }

        }
        return false;
    }

    private List<Vector2Int> GetAttackRange()
    {
        List<Vector2Int> aRange = new List<Vector2Int>();
        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                if (finished2.MapManager.Instance.map.ContainsKey(new Vector2Int(controller.target.standingOnTile.gridLocation.x + x, controller.target.standingOnTile.gridLocation.y + y))
                && Mathf.Abs(x) + Mathf.Abs(y) <= 1)
                {
                    aRange.Add(new Vector2Int(controller.target.standingOnTile.gridLocation.x + x, controller.target.standingOnTile.gridLocation.y + y));
                }
            }
        }
        return aRange;
    }

    public override List<Vector3Int> GetCoveredArea()
    {
        throw new NotImplementedException();
    }

    public override void OnDeath()
    {
        Debug.Log("Sheep died");
        Destroy(this.gameObject);
    }
}

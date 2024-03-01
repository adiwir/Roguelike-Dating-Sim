using BarthaSzabolcs.Tutorial_SpriteFlash;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class Boss : Enemy
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tilemap col;
    private List<Vector3Int> coveredArea = new();

    private int elapsedTime = 0;
    private Vector3 targetPosition;
    private Vector3 currentPosition;
    private Vector3 outOfSightPosition = new Vector3(100f, 100f);

    public GameObject firePrefab;
    public GameObject dirtParticles;

    private float movementSpeed = 4;
    private float timeThreshold = 150;
    private bool hasTransformed = false;
    private bool isTransforming = false;
    private bool inAttack = false;

    void Start()
    {
        this.maxHp = 20;
        this.hp = maxHp;
        damageFlash = GetComponent<DamageFlash>();
        dirtParticles = GameObject.FindWithTag("Dirt");
        playerTarget = GameObject.FindWithTag("Player");
        targetPosition = playerTarget.transform.position;
        coveredArea = new List<Vector3Int>();
        UpdateEnemyPosition(this.pos);
        UpdateCoveredArea();
        healthBar.UpdateHealthBar(hp, maxHp);
    }

    private void Update()
    {
        if (!(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1))
        {
            return;
        }
        currentPosition = targetPosition;
        updateTargetDirection();
    }


    void FixedUpdate()
    {
        targetPosition = playerTarget.transform.position;
        if (elapsedTime > timeThreshold)
        {
            float distance = Vector3.Distance(targetPosition, transform.position);
            if(distance < 50 && !inAttack && !isTransforming)
            {
                inAttack = true;
                StartCoroutine(Burrow(targetPosition));
            } 
        }

        if (hp <= 10 && !hasTransformed && !inAttack)
        {
            animator.SetTrigger("Transform");
            WaitForTransformation();
            movementSpeed = 8;
            hasTransformed = true;
            timeThreshold = 50;
        }
        this.pos = tilemap.WorldToCell(transform.position);
        UpdateCoveredArea();
        elapsedTime += 1;
    }

    private IEnumerator WaitForTransformation()
    {
        isTransforming = true;
        yield return new WaitForSeconds(2.2f);
        isTransforming = false;
    }

    private IEnumerator Burrow(Vector3 playerPos)
    {
        animator.SetBool("isBurrowing", true);
        yield return new WaitForSeconds(1.1f);
        transform.position = Vector3.MoveTowards(transform.position, outOfSightPosition, 10000000000000);
        dirtParticles.transform.position = Vector3.MoveTowards(dirtParticles.transform.position, playerPos, 10000000000);
        animator.SetBool("isBurrowing", false);
        yield return new WaitForSeconds(2);
        animator.SetBool("isUnburrowing", true);
        dirtParticles.transform.position = Vector3.MoveTowards(dirtParticles.transform.position, outOfSightPosition, 10000000000);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerPos.x, playerPos.y, 5), 100000000000000000);

        if (hasTransformed)
        {
            SpawnFire();
        }
        yield return new WaitForSeconds(1.1f);
        animator.SetBool("isUnburrowing", false);
        updateTargetDirection();
        yield return Charge(new Vector3(currentPosition.x, currentPosition.y, 5));
    }

    private IEnumerator Charge(Vector3 playerPos)
    {
        animator.SetBool("isCharging", true);
        Vector3 pos = transform.position;
        while (pos != playerPos)
        {
            pos = Vector3.MoveTowards(transform.position, playerPos, movementSpeed * Time.fixedDeltaTime);
            transform.position = pos;
            yield return null;
            
        }
        animator.SetBool("isCharging", false);
        inAttack = false;
        elapsedTime = 0;
    }

    private void SpawnFire()
    {
        for(int i = 0; i < 15; i++)
        {
            Vector3 posToAdd = new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f),UnityEngine.Random.Range(-0.5f, 0.5f), 0);
            Instantiate(firePrefab, transform.position + posToAdd, transform.rotation);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerTarget.GetComponentInParent<Health>().decreaseHealthPoints(1);
        }
    }

    private void UpdateCoveredArea()//kalla p� denna n�r bossen r�r sig
    {
        coveredArea.Clear();
        coveredArea.Add(this.pos);

        for (int x = pos.x - 1; x <= pos.x + 1; x++)
        {
            for (int y = pos.y - 1; y <= pos.y + 1; y++)
            {
                for (int z = pos.z - 1; z <= pos.z + 1; z++)
                {
                    coveredArea.Add(new Vector3Int(x, y, z));
                }
            }
        }
    }

    public override List<Vector3Int> GetCoveredArea()
    {
        return this.coveredArea;
    }

    public override void OnDeath()
    {
        Debug.Log("Bullmole died, RIP BullMole. You were like a father to me");
        Destroy(this.gameObject);
    }

    //private void UpdateCoveredTiles(Vector3 newPosition)
    //{
    //    EnemyPosStorage.Instance.AddEnemy(this);
    //}

}
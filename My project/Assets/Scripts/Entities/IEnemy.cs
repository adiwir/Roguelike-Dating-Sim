using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    public Vector3Int getPos();
    public List<Vector3Int> GetCoveredArea();

    public void TakeDamage(int damage);

    public void OnDeath();
}



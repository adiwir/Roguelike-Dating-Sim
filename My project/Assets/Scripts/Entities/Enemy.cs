using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Enemy
{
    public Vector3Int getPos();

    public void takeDamage(int damage);
}


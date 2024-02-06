using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicAbility : ScriptableObject, Ability
{
    string Name;
    //public __ image
    public abstract void useAbility();

}
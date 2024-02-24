using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveAbility : ScriptableObject, Ability
{
    string Name;
    public string Description;
    //public __ image
    public abstract void UseAbility();


}
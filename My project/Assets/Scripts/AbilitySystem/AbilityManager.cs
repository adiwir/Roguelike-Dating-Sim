using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    //private static AbilityManager _instance;
    //public static AbilityManager Instance { get { return _instance; } }

    private Queue<ActiveAbility> ActiveAbilities = new();
    private List<ActiveAbility> UsedAbilities = new();

    [SerializeField] private int TotalAbilityAmount = 6;
    private readonly int startingAbilityAmount = 3;

    private void Awake()
    {
        //if (_instance != null && _instance != this)
        //{
        //    Destroy(this.gameObject);
        //}
        //else
        //{
        //    _instance = this;
        //}
        AssignAbilities();
    }

    private void Start()
    {
        //AssignAbilities();
    }

    private void AssignAbilities()
    {
        for(int i = 0; i < TotalAbilityAmount; i++)
        {
            //print("did we get here");
            ActiveAbilities.Enqueue(GetNotRandomAbility()); //TODO: Make random
            //Debug.Log("daheck " + ActiveAbilities.Count);
        }
        //ActiveAbilities.Enqueue(new StickyBomb());
    }

    private ActiveAbility GetNotRandomAbility()
    {
        return (new StickyBomb());
    }

    public Queue<ActiveAbility> GetAbilityQueue() 
    {
        return this.ActiveAbilities;
    }

    public void SendActiveToDiscard(ActiveAbility activeAbility)
    {
        UsedAbilities.Add(activeAbility);
    }

}
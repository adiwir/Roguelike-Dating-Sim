using System;
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
    public bool HasADiscardedAbility { get; set; }

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
        HasADiscardedAbility = false;
    }

    private void AssignAbilities()
    {
        for(int i = 0; i < TotalAbilityAmount; i++)
        {
            ActiveAbilities.Enqueue(ChooseRandomAbility()); ;
        }
    }

    public Queue<ActiveAbility> GetAbilityQueue() 
    {
        return this.ActiveAbilities;
    }

    public void SendActiveToDiscard(ActiveAbility activeAbility)
    {
        HasADiscardedAbility = true;
        UsedAbilities.Add(activeAbility);
    }

    public Queue<ActiveAbility> Recharge()
    {
        return ShuffleAndEnqueue(UsedAbilities);
    }

    private static Queue<ActiveAbility> ShuffleAndEnqueue<ActiveAbility>(List<ActiveAbility> list)
    {
        System.Random random = new();
        int n = list.Count;

        //Fisher-Yates shuffle algorithm
        for (int i = n - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);
            ActiveAbility temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        // Enqueue shuffled items into a queue
        Queue<ActiveAbility> queue = new(list);
        return queue;
    }

    private ActiveAbility ChooseRandomAbility()
    {
            System.Random random = new System.Random();

            int randomNumber = random.Next(1, 5); // Generate a random number between 1 and 4

                switch (randomNumber)
                {
                    case 1:
                        return new StickyBomb();
                        //return new Sniper();
                    case 2:
                        //return new Shield();
                        return new StickyBomb();
                    case 3:
                        return new StickyBomb();
                        //return new Sniper();
                    case 4:
                        //return new Shield();
                        return new StickyBomb();
                    default:
                        throw new InvalidOperationException("Unexpected random number");
                }
    }  
}

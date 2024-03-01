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
        //ChooseRandomAbility();
        AssignAbilities();
        HasADiscardedAbility = false;
    }

    //private void Start()
    //{
    //    //AssignAbilities();
    //}

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
        //return new Shield();
        return (new StickyBomb());
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

    //private void ChooseUsableAbilities()
    //{
    //    List<Type> typeList = new List<Type>
    //    {
    //        typeof(StickyBomb)
    //    }
    ////UnlockedAbilities.Add()
    ///
    //private ActiveAbility ChooseRandomAbility()
    //{
    //    System.Random random = new System.Random();
    //    int randomNumber = random.Next(1, 5); // Generate a random number between 1 and 4

    //    switch (randomNumber)
    //    {
    //        case 1:
    //            return new StickyBomb();
    //        case 2:
    //            return new ClassB();
    //        case 3:
    //            return new ClassC();
    //        case 4:
    //            return new ClassD();
    //        default:
    //            throw new InvalidOperationException("Unexpected random number");
    //    }
    //}
}
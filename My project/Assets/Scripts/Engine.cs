using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class Engine : MonoBehaviour
{
    public static Engine engine;
    public Tilemap map;
    
    public Character character;
    //public List<Enemies> enemies;


    void Awake()
    {
        if (map == null) //singleton pattern
        {
            map = GameObject.FindObjectOfType<Tilemap>();
            //map.CompressBounds();
        }
        engine = this;

        character = GameObject.FindAnyObjectByType<Character>();
        //enemies = new List<Enemies>();

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSprite : MonoBehaviour
{
    // Start is called before the first frame update

    private static ShieldSprite _instance;
    public static ShieldSprite Instance { get { return _instance; } }

    // The material that was in use, when the script started.
    

    // The currently running coroutine.
    private Coroutine shieldRoutine;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        
    }
}

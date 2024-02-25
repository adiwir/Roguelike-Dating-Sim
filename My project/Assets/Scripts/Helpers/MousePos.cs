using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePos : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    public Vector3 pos;

    private static MousePos _instance;
    public static MousePos Instance { get { return _instance; } }

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

    void FixedUpdate()
    {
        pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    public Vector3 GetMousePos() {return this.pos;}
}
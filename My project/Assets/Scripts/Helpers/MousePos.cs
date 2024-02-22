using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePos : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    public Vector3 pos;

    void FixedUpdate()
    {
        pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    public Vector3 GetMousePos() {return this.pos;}
}
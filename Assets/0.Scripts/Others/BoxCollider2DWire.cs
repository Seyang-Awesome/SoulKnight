using System;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollider2DWire : MonoBehaviour
{
    private Vector2 center;
    private Vector2 box;
    private float angle;
    
    public void Set(Vector2 center, Vector2 box, float angle)
    {
        this.center = center;
        this.box = box;
        this.angle = angle;
    }
    private void Update()
    {
        transform.position = center;
        transform.localScale = box;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}


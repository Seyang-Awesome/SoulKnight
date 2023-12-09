using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityInfo : MonoBehaviour
{
    [Header("实体通用")]
    
    public Collider2D trigger;
    public Collider2D target;
    public float moveSpeed;
    public float dragFactor;

    public Vector2 CenterPos => trigger.bounds.center;
    public Vector2 TargetPos => target.bounds.center;
    public Vector2 TargetDirection => TargetPos - CenterPos;
    public int TargetDirectionCoefficient => TargetDirection.x >= 0 ? 1 : -1;
    public virtual float BuffIconHeadHeight => 2f;
}


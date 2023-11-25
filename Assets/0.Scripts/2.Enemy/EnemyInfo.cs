using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyInfo : MonoBehaviour
{
    [SerializeField]
    private Collider2D self;
    public Vector2 CenterPos => self.bounds.center;
    
    public Collider2D target;
    public Vector2 TargetPos => target.bounds.center;
    
    public float moveSpeed;
    public float dragFactor;

    public float detectPlayerRadius;

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     Debug.Log(111);
    // }
    //
    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     Debug.Log(222);
    // }
    
#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(CenterPos, detectPlayerRadius);
    }

#endif
}


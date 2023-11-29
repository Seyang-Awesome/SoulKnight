using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_21001 : EnemyController
{
    [Header("21001")] 
    
    [SerializeField]
    private float attackRadius;
    [SerializeField]
    private int attackPower;
    public override void Attack()
    {
        base.Attack();
        var attackTargets = Physics2D.OverlapCircleAll(info.CenterPos,attackRadius,Consts.EnemyTargetLayerMask);
        foreach (var attackTarget in attackTargets)
        {
            if (attackTarget.CompareTag("PlayerTeam"))
            {
                HurtInfo hurtInfo = new HurtInfo(attackPower, 
                    (Vector2)attackTarget.bounds.center - info.CenterPos);
                attackTarget.GetComponent<Hurtable>().Hurt(hurtInfo);
            }
        }
    }
    
// #if UNITY_EDITOR
//
//     private void OnDrawGizmos()
//     {
//         Gizmos.color = Color.red;
//         Gizmos.DrawWireSphere(info.CenterPos, attackRadius);
//     }
//
// #endif
}


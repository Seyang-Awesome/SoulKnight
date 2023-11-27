using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtHandler : Hurtable
{
    private EnemyInfo enemyInfo;

    private void Start()
    {
        enemyInfo = GetComponentInParent<EnemyInfo>();
    }

    public override void Hurt(HurtInfo hurtInfo)
    {
        enemyInfo.Hurt(hurtInfo);
        Flash();
        if(enemyInfo.backable)
            Back(hurtInfo);
    }
}


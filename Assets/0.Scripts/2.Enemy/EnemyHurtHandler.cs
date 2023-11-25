using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtHandler : Hurtable
{
    [SerializeField] 
    private EnemyInfo enemyInfo;
    public override void Hurt(HurtInfo hurtInfo)
    {
        enemyInfo.Hurt(hurtInfo);
        Flash();
        if(enemyInfo.backable)
            Back(hurtInfo);
    }
}


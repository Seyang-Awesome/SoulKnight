using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtHandler : Hurtable
{
    [SerializeField] private PlayerInfo playerInfo;
    public override void Hurt(HurtInfo hurtInfo)
    {
        playerInfo.Hurt(hurtInfo);
        Flash();
    }
    
}


using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomBattleInfo
{
    public int CurrentEnemyNum { get; private set; }

    public RoomBattleInfo(int currentEnemyNum)
    {
        CurrentEnemyNum = currentEnemyNum;
    }

    public void MinusEnemy()
    {
        CurrentEnemyNum--;
    }
}


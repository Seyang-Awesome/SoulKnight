using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class ProjectExtension
{
    public static Tilemap GetTilemap(this Transform transform, TilemapLayer layer)
    {
        return transform.GetChild((int)layer).GetComponent<Tilemap>();
    }

    public static int GetRelevantBulletLayer(this Team team)
    {
        if (team == Team.Player)
            return Consts.PlayerBulletLayer;
        else
            return Consts.EnemyBulletLayer;
    }

    public static int GetRelevantTargetLayerMask(this Team team)
    {
        if (team == Team.Player)
            return Consts.PlayerTargetLayerMask;
        else
            return Consts.EnemyTargetLayerMask;
    }
}


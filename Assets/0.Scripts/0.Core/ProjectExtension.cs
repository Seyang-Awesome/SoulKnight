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
}


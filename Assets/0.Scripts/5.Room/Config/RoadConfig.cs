using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Map/RoadConfig")]
public class RoadConfig : ScriptableObject
{
    public List<TileBase> backgroundTiles;
    public List<RoadMatch> roadMatches;
    public TileBase shadow;
}

[Serializable]
public class RoadMatch
{
    public TileBase wallTile;
    public TileBase foregroundTile;
}

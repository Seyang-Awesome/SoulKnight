using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Map/RoomConfig")]
public class RoomConfig : ScriptableObject
{
    public List<Room> startRooms;
    public List<Room> endRooms;
    public List<Room> commonRooms;
    public List<Room> chestRooms;
    public List<Room> storeRooms;
    public List<Room> bossRooms;
    // public RoadConfig roadConfig;
}


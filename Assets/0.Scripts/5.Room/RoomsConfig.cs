using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomsConfig : ScriptableObject
{
    public List<Room> startRooms;
    public List<Room> endRooms;
    public List<Room> commonRooms;
    public List<Room> specialRooms;
}


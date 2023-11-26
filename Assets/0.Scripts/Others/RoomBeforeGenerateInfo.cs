using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomBeforeGenerateInfo
{
    public Vector2Int Point;
    public RoomType RoomType;
    public List<RoomBeforeGenerateInfo> LinkedRooms = new();

    public RoomBeforeGenerateInfo(Vector2Int point, RoomType roomType)
    {
        this.Point = point;
        this.RoomType = roomType;
    }
    public RoomBeforeGenerateInfo(Vector2Int point, RoomType roomType, RoomBeforeGenerateInfo linkedRoom)
    {
        this.Point = point;
        this.RoomType = roomType;
        LinkedRooms.Add(linkedRoom);
    }

    public void AddLinkedRoom(RoomBeforeGenerateInfo linkedRoom)
    {
        LinkedRooms.Add(linkedRoom);
    }
}


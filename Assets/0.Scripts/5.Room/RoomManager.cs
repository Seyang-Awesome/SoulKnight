using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private int mainRoadRoomMinCount;
    [SerializeField] private int mainRoadRoomMaxCount;
    [SerializeField] private int specialRoomMinCount;
    [SerializeField] private int specialRoomMaxCount;
    [SerializeField] private List<RoomType> specialRoomTypes;

    [SerializeField] private RoomConfig roomConfig;

    [SerializeField]
    private Transform mapRoot;
    private Tilemap background;
    private Tilemap wall;
    private Tilemap foreground;
    private Tilemap shadow;
    private Tilemap door;

    private void Start()
    {
        this.background = mapRoot.GetTilemap(TilemapLayer.Background);
        this.wall = mapRoot.GetTilemap(TilemapLayer.Wall);
        this.foreground = mapRoot.GetTilemap(TilemapLayer.Foreground);
        this.shadow = mapRoot.GetTilemap(TilemapLayer.Shadow);
        this.door = mapRoot.GetTilemap(TilemapLayer.Door);

        Dictionary<RoomBeforeGenerateInfo, Room> rooms = SetRooms(GetRoomBeforeGenerateInfos());
        SetDoors(rooms);
        SetRoads(rooms);
    }

    #region 预处理数据，算法相关

    /// <summary>
    /// 通过随机步法来生成一系列房间的坐标和房间类型
    /// </summary>
    private List<RoomBeforeGenerateInfo> RandomWalk(int roomNum)
    {
        //如果房间数量不达标，就不生成
        if (roomNum >= 9 || roomNum <= 3) return default;

        //随机步法
        List<Vector2Int> roomPoints = new(){Vector2Int.zero};
        Vector2Int currentPoint = Vector2Int.zero;
        
        for (int i = 0; i < roomNum - 1; i++)
        {
            Vector2Int nextPoint = currentPoint + Consts.GetRandomDirection();
            if (roomPoints.Contains(nextPoint))
            {
                i--;
                continue;
            }
            else
            {
                currentPoint = nextPoint;
                roomPoints.Add(nextPoint);
            }
        }

        //按顺序生成info
        List<RoomBeforeGenerateInfo> roomBeforeGenerateInfos = new();
        for (int i = 0; i < roomNum; i++)
        {
            if (i == 0)
                roomBeforeGenerateInfos.Add(new RoomBeforeGenerateInfo(roomPoints[i], RoomType.Start));
            else if(i == roomNum - 1)
                roomBeforeGenerateInfos.Add(new RoomBeforeGenerateInfo(roomPoints[i], RoomType.End));
            else
                roomBeforeGenerateInfos.Add(new RoomBeforeGenerateInfo(roomPoints[i], RoomType.Common));
        }
        
        //设置连接的房间
        for (int i = 0; i < roomNum - 1; i++)
            roomBeforeGenerateInfos[i].AddLinkedRoom(roomBeforeGenerateInfos[i + 1]);

        return roomBeforeGenerateInfos;
    }
    
    /// <summary>
    /// 在主通道上生成一些列分支和房间类型信息
    /// </summary>
    private List<RoomBeforeGenerateInfo> SpecialWalk(int specialRoomNum,List<RoomBeforeGenerateInfo> mainRoadInfos)
    {
        //生成要生成的房间类型的列表
        List<RoomType> unSelected = specialRoomTypes;
        List<RoomType> specialRoomTypesToBeGenerate = new();
        for (int i = 0; i < specialRoomNum; i++)
        {
            RoomType toAddRoomType = unSelected[Random.Range(0, unSelected.Count)];
            unSelected.Remove(toAddRoomType);
            specialRoomTypesToBeGenerate.Add(toAddRoomType);
        }
        
        //生成要生成的特殊房间的信息
        List<RoomBeforeGenerateInfo> specialRoomBeforeGenerateInfo = new();
        while (specialRoomTypesToBeGenerate.Count > 0)
        {
            RoomType roomType = specialRoomTypesToBeGenerate[Random.Range(0, specialRoomTypesToBeGenerate.Count)];
            RoomBeforeGenerateInfo relevantInfo = mainRoadInfos[Random.Range(1, mainRoadInfos.Count - 1)];
            Vector2Int specialRoomPoint =
                relevantInfo.Point + Consts.GetRandomDirection();
            
            if (specialRoomBeforeGenerateInfo.Count(info => info.Point == specialRoomPoint) > 0 || 
                mainRoadInfos.Count(info => info.Point == specialRoomPoint) > 0) continue;

            RoomBeforeGenerateInfo newSpecialRoomInfo = new RoomBeforeGenerateInfo(specialRoomPoint, roomType);
            relevantInfo.AddLinkedRoom(newSpecialRoomInfo);
            specialRoomBeforeGenerateInfo.Add(newSpecialRoomInfo);
            specialRoomTypesToBeGenerate.Remove(roomType);
        }

        return specialRoomBeforeGenerateInfo;
    }

    /// <summary>
    /// 这个函数实际上就是把上面几个函数整合起来
    /// </summary>
    /// <returns></returns>
    private List<RoomBeforeGenerateInfo> GetRoomBeforeGenerateInfos()
    {
        List<RoomBeforeGenerateInfo> mainRoadRooms = RandomWalk(
            Random.Range(mainRoadRoomMinCount,mainRoadRoomMaxCount + 1));
        List<RoomBeforeGenerateInfo> specialRooms = SpecialWalk(
            Random.Range(specialRoomMinCount,specialRoomMaxCount + 1),mainRoadRooms);

        List<RoomBeforeGenerateInfo> roomBeforeGenerateInfos = mainRoadRooms.Union(specialRooms).ToList();
        roomBeforeGenerateInfos.ForEach(info => info.Point = info.Point * Consts.RoomDistance);
        // roomBeforeGenerateInfos.ForEach(info => Debug.Log($"{info.RoomType},{info.Point}"));
        return roomBeforeGenerateInfos;
    }

    #endregion

    #region 生成房间

    private Room SetRoom(RoomBeforeGenerateInfo info)
    {
        Room room = null;
        switch (info.RoomType)
        {
            case RoomType.Start:
                room = Instantiate(roomConfig.startRooms[Random.Range(0, roomConfig.startRooms.Count)],
                    info.Point.ToVector3(),Quaternion.identity);  
                break;
            case RoomType.End:
                room = Instantiate(roomConfig.endRooms[Random.Range(0, roomConfig.endRooms.Count)],
                    info.Point.ToVector3(),Quaternion.identity);  
                break;
            case RoomType.Common:
                room = Instantiate(roomConfig.commonRooms[Random.Range(0, roomConfig.commonRooms.Count)],
                    info.Point.ToVector3(),Quaternion.identity);  
                break;
            case RoomType.Boss:
                room = Instantiate(roomConfig.bossRooms[Random.Range(0, roomConfig.bossRooms.Count)],
                    info.Point.ToVector3(),Quaternion.identity);  
                break;
            case RoomType.Chest:
                room = Instantiate(roomConfig.chestRooms[Random.Range(0, roomConfig.chestRooms.Count)],
                    info.Point.ToVector3(),Quaternion.identity);  
                break;
            case RoomType.Store:
                room = Instantiate(roomConfig.storeRooms[Random.Range(0, roomConfig.storeRooms.Count)],
                    info.Point.ToVector3(),Quaternion.identity);  
                break;
            default:
                Debug.LogWarning("RoomManager.SetRoom：没有这种类型的房间！");
                return default;
        }

        return room.Init(info.Point,info.RoomType);
    }
    
    private Dictionary<RoomBeforeGenerateInfo,Room> SetRooms(List<RoomBeforeGenerateInfo> infos)
    {
        Dictionary<RoomBeforeGenerateInfo,Room> rooms = new();
        infos.ForEach(info =>
        {
            rooms.Add(info,SetRoom(info));
        });
        return rooms;
    }

    #endregion

    #region 生成门

    private Vector2Int GetDoorDirection(RoomBeforeGenerateInfo baseRoom, RoomBeforeGenerateInfo linkedRoom)
    {
        if (baseRoom.Point.x != linkedRoom.Point.x && baseRoom.Point.y != linkedRoom.Point.y)
        {
            Debug.LogWarning("RoomManager.GetRoomDirection：生成的房间信息有误！");
            return default;
        }

        if (baseRoom.Point.x == linkedRoom.Point.x)
            return ((baseRoom.Point - linkedRoom.Point).y > 0) ? Vector2Int.down : Vector2Int.up;
        else
            return ((baseRoom.Point - linkedRoom.Point).x > 0) ? Vector2Int.left : Vector2Int.right;
    }

    private void SetDoor(RoomBeforeGenerateInfo info, Dictionary<RoomBeforeGenerateInfo,Room> rooms)
    {
        List<RoomBeforeGenerateInfo> linkedRooms = info.LinkedRooms;
        linkedRooms.ForEach(linkedRoom =>
        {
            Vector2Int baseRoomDirection = GetDoorDirection(info, linkedRoom);
            Vector2Int linkedRoomDirection = -baseRoomDirection;
            
            rooms[info].SetDoor(baseRoomDirection);
            rooms[linkedRoom].SetDoor(linkedRoomDirection);
        });
    }

    private void SetDoors(Dictionary<RoomBeforeGenerateInfo,Room> rooms)
    {
        foreach (var room in rooms)
            SetDoor(room.Key,rooms);
    }
    
    #endregion

    private void SetRoad(Room baseRoom, Room linkedRoom)
    {
        Vector2Int roadDirection = (linkedRoom.CenterPos - baseRoom.CenterPos).ToVector2().normalized.ToVector2Int();
        Vector2Int roadVerticalDirection = roadDirection.GetVerticalDirection();
        Vector2Int startPos = baseRoom.CenterPos + (baseRoom.RoomHalfLength + 1) * roadDirection;
        Vector2Int endPos = linkedRoom.CenterPos - (linkedRoom.RoomHalfLength + 1) * roadDirection;

        int counter = 0;
        while (startPos - endPos != roadDirection)
        {
            RoadMatch roadMatch1 =
                roomConfig.roadConfig.roadMatches[Random.Range(0, roomConfig.roadConfig.roadMatches.Count)];
            RoadMatch roadMatch2 =
                roomConfig.roadConfig.roadMatches[Random.Range(0, roomConfig.roadConfig.roadMatches.Count)];

            Vector3Int wallPos1 = (Vector3Int)(startPos + roadVerticalDirection * Consts.RoadHalfWidth);
            Vector3Int wallPos2 = (Vector3Int)(startPos - roadVerticalDirection * Consts.RoadHalfWidth);
            
            wall.SetTile(wallPos1,roadMatch1.wallTile);
            wall.SetTile(wallPos2,roadMatch2.wallTile);
            foreground.SetTile(wallPos1,roadMatch1.foregroundTile);
            foreground.SetTile(wallPos2,roadMatch2.foregroundTile);
            
            if (roadDirection.y == 0)
            {
                shadow.SetTile(wallPos1,roomConfig.roadConfig.shadow);
                shadow.SetTile(wallPos2,roomConfig.roadConfig.shadow);
            }

            for (int i = -Consts.RoadHalfWidth; i <= Consts.RoadHalfWidth; i++)
            {
                TileBase backgroundTile =
                    roomConfig.roadConfig.backgroundTiles[Random.Range(0, roomConfig.roadConfig.backgroundTiles.Count)];
                background.SetTile((Vector3Int)(startPos + i * roadVerticalDirection), backgroundTile);
            }

            startPos += roadDirection;
            
            counter++;
            if (counter > 100) break;
        }
    }

    private void SetRoads(Dictionary<RoomBeforeGenerateInfo,Room> rooms)
    {
        foreach (var room in rooms)
            room.Key.LinkedRooms.ForEach(linkedRoom => SetRoad(rooms[room.Key],rooms[linkedRoom]));
    }
    
}


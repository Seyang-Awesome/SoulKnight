using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private int mainRoadRoomMinCount;
    [SerializeField] private int mainRoadRoomMaxCount;
    [SerializeField] private int specialRoomMinCount;
    [SerializeField] private int specialRoomMaxCount;
    [SerializeField] private List<RoomType> specialRoomTypes;

    private void Start()
    {
        
        
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
            RoomType toAddRoomType = unSelected[Random.Range(0, unSelected.Count - 1)];
            unSelected.Remove(toAddRoomType);
            specialRoomTypesToBeGenerate.Add(toAddRoomType);
        }
        
        //生成要生成的特殊房间的信息
        List<RoomBeforeGenerateInfo> specialRoomBeforeGenerateInfo = new();
        while (specialRoomTypesToBeGenerate.Count > 0)
        {
            RoomType roomType = specialRoomTypesToBeGenerate[Random.Range(0, specialRoomTypesToBeGenerate.Count - 1)];
            RoomBeforeGenerateInfo relevantInfo = mainRoadInfos[Random.Range(1, mainRoadInfos.Count - 2)];
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
            Random.Range(mainRoadRoomMinCount,mainRoadRoomMaxCount));
        List<RoomBeforeGenerateInfo> specialRooms = SpecialWalk(
            Random.Range(specialRoomMinCount,specialRoomMaxCount),mainRoadRooms);

        List<RoomBeforeGenerateInfo> roomBeforeGenerateInfo = mainRoadRooms.Union(specialRooms).ToList();
        roomBeforeGenerateInfo.ForEach(info => info.Point = info.Point * Consts.RoomDistance);
        return roomBeforeGenerateInfo;
    }

    #endregion

    
}


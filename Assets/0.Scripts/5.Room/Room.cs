using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    [SerializeField] private Collider2D doorCollider;

    [SerializeField] private TileBase[] doorTiles;
    
    public bool IsAwake { get; private set; }
    public bool IsBattling { get; private set; }
    public bool IsDied { get; private set; }

    public RoomType RoomType { get; private set; }
    public int RoomWidth { get; private set; }
    public int RoomHeight { get; private set; }
    public int RoomLength => RoomWidth;
    public int RoomHalfLength => (RoomLength - 1) / 2;
    public Vector2Int CenterPos { get; private set; }
    
    private Tilemap background;
    private Tilemap wall;
    private Tilemap foreground;
    private Tilemap shadow;
    private Tilemap door;
    

    public Room(RoomType roomType)
    {
        this.RoomType = roomType;
        IsAwake = false;
        IsBattling = false;
        IsDied = false;
    }

    private void Start()
    {
        Init(RoomType.Start);
    }

    public void Init(RoomType roomType)
    {
        this.RoomType = roomType;
        
        this.background = GetTilemap(TilemapLayer.Background);
        this.wall = GetTilemap(TilemapLayer.Wall);
        this.foreground = GetTilemap(TilemapLayer.Foreground);
        this.shadow = GetTilemap(TilemapLayer.Shadow);
        this.door = GetTilemap(TilemapLayer.Door);
        
        background.CompressBounds();

        var cellBounds = background.cellBounds;
        RoomWidth = cellBounds.xMax - cellBounds.xMin;
        RoomHeight = cellBounds.yMax - cellBounds.yMin; 
        if (RoomWidth != RoomHeight)
            Debug.LogWarning($"房间的长宽不一致，长度为{RoomWidth}，宽度为{RoomHeight}");
        else
            Debug.Log($"房间的长度为:{RoomLength}");
    }
    
    private void AwakeRoom()
    {
        Debug.Log("AwakeRoom");
    }

    private Tilemap GetTilemap(TilemapLayer layer)
    {
        return transform.GetChild((int)layer).GetComponent<Tilemap>();
    }
    
    public void OnExitDoor(Collider2D other)
    {
        Debug.LogWarning("la");
        if (other.gameObject.layer != Consts.PlayerTriggerLayer || IsDied) return;
        if (Mathf.Abs(CenterPos.x - other.bounds.center.x) < (float)RoomLength / 2 &&
            Mathf.Abs(CenterPos.y - other.bounds.center.y) < (float)RoomLength / 2)
        {
            AwakeRoom();
        }
    }
    
    public void SetDoor(Direction direction)
    {
        SetDoor(direction.GetRelevantDirection().ToVector2Int());
    }

    public void SetDoor(Vector2Int direction)
    {
        Vector2Int doorCenterPos = direction * RoomHalfLength;
        Vector2Int verticalPos = doorCenterPos.GetVerticalDirection();
        
        for (int i = -Consts.DoorWidth / 2; i <= Consts.DoorWidth / 2; i++)
        {
            Vector3Int relevantPos = (Vector3Int)(doorCenterPos + verticalPos * i);
            door.SetTile(relevantPos,doorTiles[0]);
            wall.SetTile(relevantPos,null);
            foreground.SetTile(relevantPos,null);
        }
    }

    [ContextMenu("LeftDoor")]
    private void SetLeftDoor()
    {
        SetDoor(Direction.Left);
    }
    
    [ContextMenu("UpDoor")]
    private void SetUpDoor()
    {
        SetDoor(Direction.Up);
    }
}

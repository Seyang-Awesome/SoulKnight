using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = System.Object;
using Random = UnityEngine.Random;
// using UnityEngine.UIElements;

public class Room : MonoBehaviour
{
    [SerializeField] private Collider2D doorCollider;
    [SerializeField] private TileBase[] doorTiles;
    
    public bool IsAwake { get; private set; }
    public bool IsDied { get; private set; }

    public RoomType RoomType { get; private set; }
    public int RoomWidth { get; private set; }
    public int RoomHeight { get; private set; }
    public int RoomLength => RoomWidth;
    public int RoomHalfLength => (RoomLength - 1) / 2;
    
    public Vector2Int CenterPos { get; private set; }
    public Vector2 CenterPosInWorld => CenterPos + new Vector2(0.5f,0.5f);
    public Vector2 CheckBox => new Vector2((float)RoomLength - 4.5f,(float)RoomLength - 4.5f);
    
    private Tilemap background;
    private Tilemap wall;
    private Tilemap foreground;
    private Tilemap shadow;
    private Tilemap door;
    private Tilemap doorHead;

    private List<Vector2> enemySpawnPosList = new();
    private List<Vector3Int> doorTilePos = new();
    private int enemySpawnWave;
    
    private void Start()
    {
        float xMin = CenterPosInWorld.x - RoomHalfLength + 2f;
        float xMax = CenterPosInWorld.x + RoomHalfLength - 2f;
        float yMin = CenterPosInWorld.y - RoomHalfLength + 2f;
        float yMax = CenterPosInWorld.y + RoomHalfLength - 2f;

        Vector2 detectBox = Consts.EnemySpawnBox;
        for (float i = xMin; i <= xMax; i++)
        {
            for (float j = yMin; j <= yMax; j++)
            {
                if(!Physics2D.BoxCast(new Vector2(i, j), detectBox, 0, 
                       Vector2.zero, 0, Consts.MapLayerMask))
                    enemySpawnPosList.Add(new Vector2(i,j));
            }
        }
        
        BoundsInt bounds = door.cellBounds;
        
        for (int x = bounds.x; x < bounds.x + bounds.size.x; x++)
        {
            for (int y = bounds.y; y < bounds.y + bounds.size.y; y++)
            {
                Vector3Int currentPos = new Vector3Int(x, y, 0);
                if (door.GetTile(currentPos) != null)
                    doorTilePos.Add(currentPos);
            }
        }
    }

    public Room Init(Vector2Int centerPos,RoomType roomType)
    {
        this.background = transform.GetTilemap(TilemapLayer.Background);
        this.wall = transform.GetTilemap(TilemapLayer.Wall);
        this.foreground = transform.GetTilemap(TilemapLayer.Foreground);
        this.shadow = transform.GetTilemap(TilemapLayer.Shadow);
        this.door = transform.GetTilemap(TilemapLayer.Door);
        this.doorHead = transform.GetTilemap(TilemapLayer.DoorHead);
        
        background.CompressBounds();

        var cellBounds = background.cellBounds;
        RoomWidth = cellBounds.xMax - cellBounds.xMin;
        RoomHeight = cellBounds.yMax - cellBounds.yMin; 
        if (RoomWidth != RoomHeight)
            Debug.LogWarning($"房间的长宽不一致，长度为{RoomWidth}，宽度为{RoomHeight}");
        // else
        //     Debug.Log($"房间的长度为:{RoomLength}");
        
        this.CenterPos = centerPos;
        this.RoomType = roomType;
        
        IsAwake = false;
        IsDied = false;
        enemySpawnWave = Random.Range(1, 4);
        
        return this;
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
            if(direction == Vector2Int.up || direction == Vector2Int.down)
                shadow.SetTile(relevantPos,null);
        }
    }
    
    public void OnExitDoor(Collider2D other)
    {
        if (other.gameObject.layer != Consts.PlayerTriggerLayer || IsDied || IsAwake) return;
        if (RoomType != RoomType.Common) return;
        
        if (Mathf.Abs(CenterPosInWorld.x - other.bounds.center.x) < (float)RoomLength / 2 &&
            Mathf.Abs(CenterPosInWorld.y - other.bounds.center.y) < (float)RoomLength / 2)
            AwakeRoom();
        // Debug.Log(CenterPosInWorld);
        // Debug.Log(other.bounds.center);
        // Debug.Log((float)RoomLength / 2);
        // Debug.Log((Mathf.Abs(CenterPosInWorld.x - other.bounds.center.x) < (float)RoomLength / 2 &&
        //            Mathf.Abs(CenterPosInWorld.y - other.bounds.center.y) < (float)RoomLength / 2));
    }
    
    private void AwakeRoom()
    {
        // Debug.Log("AwakeRoom");
        SpawnEnemy();
        CloseDoor();
        IsAwake = true;
        EventManager.Instance.OnEnemyClear += OnEnemyClear;
    }

    private void RestrainRoom()
    {
        // Debug.Log("RestrainRoom");
        OpenDoor();
        IsDied = true;
        EventManager.Instance.OnEnemyClear -= OnEnemyClear;
    }

    private void OpenDoor()
    {
        door.GetComponent<CompositeCollider2D>().isTrigger = true;
        doorTilePos.ForEach(pos => door.SetTile(pos,doorTiles[0]));
        doorTilePos.ForEach(pos => doorHead.SetTile(pos,null));
    }

    private async void CloseDoor()
    {
        door.GetComponent<CompositeCollider2D>().isTrigger = false;
        doorTilePos.ForEach(pos => door.SetTile(pos,doorTiles[1]));
        doorTilePos.ForEach(pos => doorHead.SetTile(pos,doorTiles[2]));
    }
    
    private void SpawnEnemy()
    {
        int enemyNum = Random.Range(4, 8);
        enemySpawnWave--;
        EventManager.Instance.OnRequestSpawnEnemy.Invoke(enemySpawnPosList,5);
    }

    private void OnEnemyClear()
    {
        if (enemySpawnWave > 0)
            SpawnEnemy();
        else
            RestrainRoom();
    }

    [ContextMenu("SetDoor")]
    private void SetLeftDoor()
    {
        SetDoor(Vector2Int.left);
        SetDoor(Vector2Int.right);
        SetDoor(Vector2Int.up);
        SetDoor(Vector2Int.down);

    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(CenterPosInWorld, CheckBox);
    }
#endif
}

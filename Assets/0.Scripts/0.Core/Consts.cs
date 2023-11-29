using System.Collections.Generic;
using UnityEngine;

public class Consts
{
    public const float TinyNum = 0.1f;
    public const float LargeNum = 9999999999f;
    
    public const float BulletDisappearTime = 5f;

    public const float FlashTime = 0.03f;
    public const string FlashAmount = "_FlashAmount";
    public const float BackVelocity = 8f;
    public const float BackTime = 0.1f;

    public const int EnemySpriteIndex = 0;
    public const int EnemyColliderIndex = 1;
    public const int EnemyTriggerIndex = 2;
    public const int EnemyWeaponIndex = 3;
    
    public const float EnemyDieBasicVelocity = 50;
    public const float EnemyDieFlyTime = 1;

    public const string PlayerTeamTag = "PlayerTeam";
    public const string EnemyTeamTag = "EnemyTeam";
    public const string BoxTag = "Box";

    public const int PlayerLayer = 6;
    public const int EnemyLayer = 7;
    public const int PetLayer = 8;
    public const int PlayerBulletLayer = 11;
    public const int EnemyBulletLayer = 12;
    public const int WallLayer = 16;
    public const int DoorLayer = 17;
    public const int PlayerColliderLayer = 21;
    public const int EnemyColliderLayer = 22;
    public const int PlayerTriggerLayer = 26;
    public const int EnemyTriggerLayer = 27;

    public const int BackgroundIndex = 0;
    public const int WallIndex = 1;
    public const int ForegroundIndex = 2;
    public const int ShadowIndex = 3;
    public const int DoorIndex = 4;
    public const int DoorHeadIndex = 5;

    public const float WallDetectLength = 1f;
    
    public const int RoomDistance = 25;
    public const int DoorWidth = 5;
    public static Vector2 EnemySpawnBox => new Vector2(0.95f,0.95f);
    public const float EnemySpawnDelayTime = 1f;
    
    public static int DoorHalfWidth => DoorWidth / 2;
    public const int RoadWidth = 7;
    public static int RoadHalfWidth = RoadWidth / 2;

    public static int EnemyTargetLayerMask => 1 << PlayerTriggerLayer;
    public static int MapLayerMask => 1 << WallLayer | 1 << DoorLayer;

    public static List<Vector2Int> Directions = new()
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };

    public static Vector2Int GetRandomDirection()
    {
        return Directions[Random.Range(0, Directions.Count - 1)];
    }
    
    
    
}
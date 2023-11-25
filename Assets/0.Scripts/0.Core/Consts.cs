public class Consts
{
    public const float TinyNum = 0.1f;
    public const float BulletDisapperTime = 5f;

    public const float FlashTime = 0.03f;
    public const string FlashAmout = "_FlashAmount";
    public const float BackCoffient = 15;

    public const string PlayerTeamTag = "PlayerTeam";
    public const string EnemyTeamTag = "EnemyTeam";

    public const int PlayerLayer = 6;
    public const int EnemyLayer = 7;
    public const int PetLayer = 8;
    public const int PlayerBulletLayer = 11;
    public const int EnemyBulletLayer = 12;
    public const int MapLayer = 16;
    public const int PlayerColliderLayer = 21;
    public const int EnemyColliderLayer = 22;
    public const int PlayerTriggerLayer = 26;
    public const int EnemyTriggerLayer = 27;

    public static int EnemyTargetLayerMask => 1 << PlayerTriggerLayer;
}
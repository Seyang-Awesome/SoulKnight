public enum Team
{
    Player,
    Enemy,
}
public enum AnimationType
{
    Idle,
    Move,
    Die
}

public enum RoomType
{
    Start,
    End,
    Common,
    Boss,

    Store,
    Chest,
}

public enum RoomSize
{
    Small,
    Middle,
    Large,
}

public enum TilemapLayer
{
    Background,
    Wall,
    Foreground,
    Shadow,
    Door,
    DoorHead,
}

public enum Direction
{
    Up = 0x001,
    Down = 0x002,
    Left = 0x004,
    Right = 0x008
}

public enum BuffType
{
    Poison = 0x001,
    Fire = 0x002,
    Ice = 0x004,
    SpeedUp = 0x008,
    SlowDown = 0x016,
}

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
}

public enum Direction
{
    Up = 0x001,
    Down = 0x002,
    Left = 0x004,
    Right = 0x008
}
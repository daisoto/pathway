using UnityEngine;

namespace Gameplay
{ 
public class Cell
{
    public Vector2Int Position { get; }
    public Direction Direction { get; set; }
    
    public Cell(Vector2Int position)
    {
        Position = position;
        Direction = Direction.None;
    }
}
}
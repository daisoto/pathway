using UnityEngine;

namespace Gameplay
{ 
public class Cell
{
    public Vector2Int Index { get; }
    public Direction Direction { get; set; }
    
    public Cell(Vector2Int index)
    {
        Index = index;
        Direction = Direction.None;
    }
}
}
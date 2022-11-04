using UnityEngine;

namespace Gameplay
{
public class LeftCellProvider:  NextCellProvider
{
    public LeftCellProvider(Cell[,] cells) : base(cells) { }

    public override Cell GetNextCell(Vector2Int index)
    {
        var nextX = index.x - 1;
        
        return nextX > 0 ? _cells[nextX, index.y] : _cells[index.x, index.y];
    }
}
}
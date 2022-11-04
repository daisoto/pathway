using UnityEngine;

namespace Gameplay
{ 
public class RightCellProvider: NextCellProvider
{
    public RightCellProvider(Cell[,] cells) : base(cells) { }

    public override Cell GetNextCell(Vector2Int index)
    {
        var nextX = index.x + 1;
        
        return nextX < _cells.GetLength(0) ? 
            _cells[nextX, index.y] : _cells[index.x, index.y];
    }
}
}
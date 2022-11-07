using UnityEngine;

namespace Gameplay
{
public class UpperCellProvider: NextCellProvider
{
    public UpperCellProvider(Cell[,] cells) : base(cells) { }

    public override Cell GetNextCell(Vector2Int index)
    {
        var nextY = index.y + 1;
        
        return nextY < _cells.GetLength(1) ? 
            _cells[index.x, nextY] : _cells[index.x, index.y];
    }
}
}
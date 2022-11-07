using UnityEngine;

namespace Gameplay
{
public class LowerCellProvider: NextCellProvider
{
    public LowerCellProvider(Cell[,] cells) : base(cells) { }

    public override Cell GetNextCell(Vector2Int index)
    {
        var nextY = index.y - 1;
        
        return nextY >= 0 ? _cells[index.x, nextY] : _cells[index.x, index.y];
    }
}
}
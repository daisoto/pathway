using UnityEngine;

namespace Gameplay
{
public abstract class NextCellProvider
{
    protected readonly Cell[,] _cells;

    protected NextCellProvider(Cell[,] cells)
    {
        _cells = cells;
    }
    
    public abstract Cell GetNextCell(Vector2Int index);
}
}
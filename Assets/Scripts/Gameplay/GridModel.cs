using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{ 
public class GridModel 
{
    private readonly Vector2Int _size;
    private readonly Cell[,] _cells;
    private readonly Dictionary<Direction, NextCellProvider> _nextCellProviders;

    public GridModel(Vector2Int size)
    {
        _size = size;
        _cells = new Cell[size.x, size.y];
        
        _nextCellProviders = new Dictionary<Direction, NextCellProvider>
        {
            { Direction.Down, new LowerCellProvider(_cells) }, 
            { Direction.Up, new UpperCellProvider(_cells) }, 
            { Direction.Left, new LeftCellProvider(_cells) }, 
            { Direction.Right, new RightCellProvider(_cells) }, 
        };
    }
    
    public void CreateGrid()
    {
        for (int x = 0; x < _size.x; x++)
        for (int y = 0; y < _size.y; y++)
            _cells[x, y] = new Cell(new Vector2Int(x, y));
    }
    
    public Cell GetCell(Vector2Int position) => _cells[position.x, position.y];

    public Cell GetNextCell(Vector2Int position)
    {
        var cell = GetCell(position);
        var direction = cell.Direction;
        return direction == Direction.None ? cell :
            _nextCellProviders[direction].GetNextCell(cell);
    }
}
}
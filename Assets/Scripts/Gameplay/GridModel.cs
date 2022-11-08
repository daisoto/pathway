using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{ 
public class GridModel 
{
    private readonly Vector2Int _size;
    private readonly Cell[,] _cells;
    private readonly Dictionary<Direction, NextCellProvider> _nextCellProviders;
    private readonly DistantIndexProvider xDistantIndexProvider;
    private readonly DistantIndexProvider yDistantIndexProvider;
    
    public IList<Cell> OccupiedCells => _occupiedCells;
    private readonly List<Cell> _occupiedCells;

    public GridModel(Vector2Int size)
    {
        _size = size;
        _cells = new Cell[size.x, size.y];
        
        xDistantIndexProvider = new DistantIndexProvider(
            0, size.x -1);
        yDistantIndexProvider = new DistantIndexProvider(
            0, size.y -1);
        
        _nextCellProviders = new Dictionary<Direction, NextCellProvider>
        {
            { Direction.Down, new LowerCellProvider(_cells) }, 
            { Direction.Up, new UpperCellProvider(_cells) }, 
            { Direction.Left, new LeftCellProvider(_cells) }, 
            { Direction.Right, new RightCellProvider(_cells) }, 
        };
        
        _occupiedCells = new List<Cell>();
    }
    
    public void CreateGrid()
    {
        for (int x = 0; x < _size.x; x++)
        for (int y = 0; y < _size.y; y++)
            _cells[x, y] = new Cell(new Vector2Int(x, y));
    }
    
    public Cell GetCell(Vector2Int index) => _cells[index.x, index.y];

    public Cell GetNextCell(Vector2Int index)
    {
        var cell = GetCell(index);
        var direction = cell.Direction;
        return direction == Direction.None ? cell :
            _nextCellProviders[direction].GetNextCell(index);
    }
    
    public (Cell, Cell) GetFiniteCells(Func<int> distanceProvider)
    {
        var (initialCell, finalCell) = GetFiniteCellsInternal(distanceProvider);
            
        while (_occupiedCells.Contains(initialCell) ||
               _occupiedCells.Contains(finalCell))
            (initialCell, finalCell) = GetFiniteCellsInternal(distanceProvider);
            
        _occupiedCells.Add(initialCell);
        _occupiedCells.Add(finalCell);
        
        return (initialCell, finalCell);
    }
    
    private (Cell, Cell) GetFiniteCellsInternal(Func<int> distanceProvider)
    {
        var distance = distanceProvider.Invoke();
        var startingCell = GetRandomCell();
        var finalCell = GetEquidistantCell(startingCell.Index, distance);
        
        return (startingCell, finalCell);
    }
    
    private Cell GetRandomCell()
    {
        var x = RandomUtils.GetInt(0, _size.x - 1);
        var y = RandomUtils.GetInt(0, _size.y - 1);
        
        return _cells[x, y];
    }
    
    private Cell GetEquidistantCell(Vector2Int initialPosition, int distance)
    {
        var xDistance = RandomUtils.GetInt(0, distance);
        var yDistance = distance - xDistance;
        
        var x = xDistantIndexProvider
            .GetIndex(initialPosition.x, xDistance);
        var y = yDistantIndexProvider
            .GetIndex(initialPosition.y, yDistance);
        
        return _cells[x, y];
    }
}
}
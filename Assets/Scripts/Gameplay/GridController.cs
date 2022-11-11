using System;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Gameplay
{
public class GridController
{
    private readonly GridSettings _gridSettings;
    private readonly CellsSettings _cellsSettings;
    private readonly GridBehaviour _behaviour;
    private readonly GridModel _model;
        
    private Vector2Int _size => _gridSettings.Size;

    public GridController(GridSettings gridSettings,
        GridBehaviour behaviour, CellsSettings cellsSettings)
    {
        _gridSettings = gridSettings;
        _behaviour = behaviour;
        _cellsSettings = cellsSettings;

        _model = new GridModel(_size);
    }

    public void CreateGrid()
    {
        _model.CreateGrid();
        _behaviour
            .CreateGrid(_size, ClearCell)
            .SetDirectionSetter(SetDirection);
    }
    
    private void SetDirection(Vector2Int pos, Direction dir)
    {
        _model.SetCellDirection(pos, dir);
        _behaviour
            .GetCell(pos)
            .SetSprite(_cellsSettings.GetSprite(dir))
            .SetRotation(_cellsSettings.GetRotation(dir));
    }
    
    public void MarkDestination(Cell cell, Color color)
    {
        cell.Direction = Direction.None;
        
        _behaviour
            .GetCell(cell.Index)
            .SetSprite(_cellsSettings.DestinationSprite)
            .SetColor(color)
            .SetChangeable(false);
    }
    
    public void ClearCells()
    {
        var indexesToClear = new List<Vector2Int>(_model.SetCellsIndexes);
        indexesToClear.AddRange(_model.FiniteCellsIndexes);
        
        _behaviour.IterateCells(indexesToClear, ClearCell);
        _model.ClearSetCells();
        _model.ClearFiniteCells();
    } 
    
    private void ClearCell(CellBehaviour cell)
    {
        var dir = Direction.None;
        cell
            .SetColor(_cellsSettings.DefaultColor)
            .SetSprite(_cellsSettings.GetSprite(dir))
            .SetChangeable(true)
            .SetRotation(_cellsSettings.GetRotation(dir));
    }
    
    
    public Vector3 GetCellPosition(Cell cell) => 
        _behaviour.GetCell(cell.Index).Position;
    
    public Cell GetNextCell(Cell cell) => _model.GetNextCell(cell.Index);

    public (Cell, Cell) GetFiniteCells(Func<int> distanceProvider) => 
        _model.GetFiniteCells(distanceProvider);
}
}
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
            .CreateGrid(_size)
            .SetDirectionSetter(SetDirection);
    }
    
    private void SetDirection(Vector2Int pos, Direction dir)
    {
        _model.GetCell(pos).Direction = dir;
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
    
    public void ClearCell(Cell cell)
    {
        _behaviour
            .GetCell(cell.Index)
            .SetColor(_cellsSettings.DefaultColor)
            .SetSprite(_cellsSettings.GetSprite(Direction.None))
            .SetChangeable(true);
    }
    
    public Vector3 GetCellPosition(Cell cell) => 
        _behaviour.GetCell(cell.Index).Position;
    
    public Cell GetNextCell(Cell cell) => _model.GetNextCell(cell.Index);

    public (Cell, Cell) GetFiniteCells(int distance) => 
        _model.GetFiniteCells(distance);
}
}
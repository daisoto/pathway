using Data;
using UnityEngine;

namespace Gameplay
{
public class GridController
{
    private readonly GridSettings _settings;
    private readonly GridBehaviour _behaviour;
    private readonly GridModel _model;
        
    private Vector2Int _size => _settings.Size;

    public GridController(GridSettings settings,
        GridBehaviour behaviour)
    {
        _settings = settings;
        _behaviour = behaviour;
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
        var data = _settings.GetCellData(dir);
        _model.GetCell(pos).Direction = dir;
        _behaviour
            .GetCell(pos)
            .SetSprite(_settings.CellSprite)
            .SetRotation(data.Rotation);
    }
    
    public void MarkDestination(Cell cell, Sprite sprite, Color color)
    {
        _behaviour
            .GetCell(cell.Index)
            .SetSprite(sprite)
            .SetColor(color)
            .SetChangeable(false);
    }
    
    public void ClearCell(Cell cell)
    {
        _behaviour
            .GetCell(cell.Index)
            .SetSprite(_settings.CellSprite)
            .SetChangeable(true);
    }
    
    public Vector3 GetCellPosition(Cell cell) => 
        _behaviour.GetCell(cell.Index).Position;
    
    public Cell GetNextCell(Cell cell) => _model.GetNextCell(cell.Index);
    
    public Cell GetRandomCell() => _model.GetRandomCell();
    
    public Cell GetEquidistantCell(Vector2Int initialPosition, int distance) => 
        _model.GetEquidistantCell(initialPosition, distance);
}
}
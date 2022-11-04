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
        
        CreateGrid();
    }

    public void CreateGrid()
    {
        _model.CreateGrid();
        _behaviour
            .CreateGrid(_size)
            .SetOnEnter(Enter)
            .SetDirectionSetter(SetDirection);
    }
    
    private void Enter(Vector2Int pos)
    {
        var next = _model.GetNextCell(pos);
        // todo with movers
    }
    
    private void SetDirection(Vector2Int pos, Direction dir)
    {
        var data = _settings.GetCellData(dir);
        _model.GetCell(pos).Direction = dir;
        var behaviour = _behaviour.GetCell(pos);
        
        behaviour
            .SetSprite(_settings.CellSprite)
            .SetRotation(data.Rotation)
            .SetColor(data.Color);
    }
    
    public Cell GetRandomCell() => _model.GetRandomCell();
    
    public Cell GetEquidistantCell(Vector2Int initialPosition, int distance) => 
        _model.GetEquidistantCell(initialPosition, distance);
}
}
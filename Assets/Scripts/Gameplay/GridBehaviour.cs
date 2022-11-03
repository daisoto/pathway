using System;
using UnityEngine;

namespace Gameplay
{
public class GridBehaviour : MonoBehaviour
{
    [SerializeField] 
    private CellBehaviour _cellPrefab;
    
    private Action<Vector2Int> _onEnter;
    private Action<Vector2Int, Direction> _setDirection;

    private CellBehaviour[,] _cells;

    public GridBehaviour CreateGrid(Vector2Int size)
    {
        _cells = new CellBehaviour[size.x, size.y];
        var scale = transform.localScale;
        var cellScale = new Vector3(scale.x / size.x, 0, scale.z / size.y);
            
        for (int x = 0; x < size.x; x++)
        for (int y = 0; y < size.y; y++)
            CreateCell(new Vector2Int(x, y), cellScale);
        
        return this;
    }
    
    private void CreateCell(Vector2Int position, Vector3 scale)
    {
        var cell = Instantiate(_cellPrefab, transform);
        cell
            .SetOnEnter(() => _onEnter?.Invoke(position))
            .SetDirectionSetter(dir => 
                _setDirection?.Invoke(position, dir))
            .Scale = scale;
        cell.Position = new Vector3(
            position.x * scale.x, 0, 
            position.y * scale.z); // todo pivot
            
        _cells[position.x, position.y] = cell;
    }
    
    public GridBehaviour SetOnEnter(Action<Vector2Int> onEnter)
    {
        _onEnter = onEnter;
        
        return this;
    }
    
    public GridBehaviour SetDirectionSetter(
        Action<Vector2Int, Direction> setDirection)
    {
        _setDirection = setDirection;
        
        return this;
    }
        
    public CellBehaviour GetCell(Vector2Int position) => 
        _cells[position.x, position.y];
}
}
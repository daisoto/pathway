using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
public class GridBehaviour : MonoBehaviour
{
    public Vector3 Position => transform.position;
    
    [SerializeField] 
    private CellBehaviour _cellPrefab;
    
    [SerializeField]
    private float _upperIndent;
    
    private Action<Vector2Int, Direction> _setDirection;

    private CellBehaviour[,] _cells;

    public GridBehaviour CreateGrid(Vector2Int size, 
        Action<CellBehaviour> onCreate)
    {
        _cells = new CellBehaviour[size.x, size.y];
        var cellSize = new Vector2(1f / size.x, 1f / size.y);
        var start = new Vector3(
            cellSize.x / 2 - .5f, 
            transform.localScale.y / 2 + _upperIndent, 
            cellSize.y / 2 - .5f);
            
        for (int x = 0; x < size.x; x++)
        for (int y = 0; y < size.y; y++)
        {
            var cell = CreateCell(
                new Vector2Int(x, y), cellSize, start);
            onCreate.Invoke(cell);
        }
        
        return this;
    }
    
    public void IterateCells(IEnumerable<Vector2Int> indexes,
        Action<CellBehaviour> onIterate)
    {
        foreach (var index in indexes)
            onIterate.Invoke(_cells[index.x, index.y]);
    }
    
    private CellBehaviour CreateCell(Vector2Int position, Vector2 size, Vector3 start)
    {
        var cell = Instantiate(_cellPrefab, transform);
        var scale = transform.localScale;
        var cellPosition = new Vector3(
            scale.x * (start.x + position.x * size.x), 
            scale.y * start.y, 
            scale.z * (start.z + position.y * size.y));
        cell
            .SetDirectionSetter(dir => 
                _setDirection?.Invoke(position, dir))
            .SetSize(size)
            .Position = cellPosition;
            
        _cells[position.x, position.y] = cell;
        
        return cell;
    }
    
    public GridBehaviour SetDirectionSetter(
        Action<Vector2Int, Direction> setDirection)
    {
        _setDirection = setDirection;
        
        return this;
    }
        
    public CellBehaviour GetCell(Vector2Int index) => 
        _cells[index.x, index.y];
}
}
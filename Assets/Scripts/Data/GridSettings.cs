using System;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;

namespace Data
{
[CreateAssetMenu(fileName = "New GridSettings", menuName = "Grid settings")]
public class GridSettings: ScriptableObject
{
    [SerializeField]
    private Vector2Int _size;
    public Vector2Int Size => _size;
    
    [SerializeField]
    private MoverData[] _moversData;
    public IList<MoverData> MoversData => _moversData;
    
    [SerializeField]
    private Sprite _cellSprite;
    public Sprite CellSprite => _cellSprite;
    
    [SerializeField]
    private CellData[] _cellsData;
    
    public Quaternion GetRotation(Direction dir)
    {
        foreach (var cd in _cellsData)
            if (cd.Direction == dir)
                return cd.Rotation;
        
        return default;
    }
}
    
[Serializable]
public class CellData
{
    [SerializeField]
    private Direction _direction;
    public Direction Direction => _direction; 
    
    [SerializeField]
    private Quaternion _rotation;
    public Quaternion Rotation => _rotation;
}
    
[Serializable]
public class MoverData
{
    [SerializeField]
    private Color _color;
}
}
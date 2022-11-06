using System;
using System.Linq;
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
    private Sprite _cellSprite;
    public Sprite CellSprite => _cellSprite;
    
    [SerializeField]
    private CellData[] _cellsData;
    
    public CellData GetCellData(Direction dir) => 
        _cellsData.FirstOrDefault(cd => cd.Direction == dir);
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
}
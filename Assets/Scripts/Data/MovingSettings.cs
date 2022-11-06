using System;
using UnityEngine;
using System.Collections.Generic;
using Gameplay;

namespace Data
{
[CreateAssetMenu(fileName = "New MoversSettings", menuName = "Movers settings")]
public class MoversSettings: ScriptableObject
{
    [SerializeField]
    private MoverData[] _moversData;
    public IList<MoverData> MoversData => _moversData;
    
    [SerializeField]
    private MoverBehaviour _moverPrefab;
    
    public MoverBehaviour GetMoverBehaviour() => Instantiate(_moverPrefab);
}
    
[Serializable]
public class MoverData
{
    [SerializeField]
    private Color _color;
    public Color Color => _color;
    
    [SerializeField]
    private float _speed;
    public float Speed => _speed; 
    
    [SerializeField]
    private Sprite _destinationSprite;
    public Sprite DestinationSprite => _destinationSprite;
    
    [SerializeField]
    private int _minDistance;
    
    [SerializeField]
    private int _maxDistance;
    
    public int GetDistance() => 
        RandomUtils.GetInt(_minDistance, _maxDistance);
}
}
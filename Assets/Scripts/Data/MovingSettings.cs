using System;
using UnityEngine;
using System.Collections.Generic;

namespace Data
{
[CreateAssetMenu(fileName = "New MovingSettings", menuName = "Moving settings")]
public class MovingSettings: ScriptableObject
{
    [SerializeField]
    private MovingData[] _moversData;
    public IList<MovingData> MoversData => _moversData;
}
    
[Serializable]
public class MovingData
{
    [SerializeField]
    private Color _color;
    
    [SerializeField]
    private int _minDistance;
    
    [SerializeField]
    private int _maxDistance;
    
    public int GetDistance() => 
        RandomUtils.GetInt(_minDistance, _maxDistance);
}
}
using UnityEngine;

namespace Gameplay
{
public class DistantIndexProvider
{
    private readonly int _minValue;
    private readonly int _maxValue;

    public DistantIndexProvider(int minValue, int maxValue)
    {
        _minValue = minValue;
        _maxValue = maxValue;
    }
    
    public int GetIndex(int initial, int distance)
    {
        var min =  initial - distance;
        var max = initial + distance;
        var canMin = CheckMin(min);
        var canMax = CheckMax(max);
        
        if (!canMax && !canMin)
        { // todo decorate errors?
            Debug.LogError($"Distance {distance} is too high!");
            
            return initial;
        }
        
        if (canMin && !canMax)
            return min;
        if (!canMin)
            return max;
        
        return RandomUtils.ProcessProbability(0.5) ? min : max;
    }
    
    private bool CheckMin(int value) => value >= _minValue;
    private bool CheckMax(int value) => value <= _maxValue;
}
}
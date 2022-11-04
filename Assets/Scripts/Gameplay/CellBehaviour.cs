using System;
using UnityEngine;

namespace Gameplay
{
public class CellBehaviour: MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _renderer;
    
    private Action<Direction> _setDirection;
    
    public Vector3 Position
    {
        get => transform.position;
        set => transform.position = value;
    }
    
    public CellBehaviour SetDirectionSetter(Action<Direction> setDirection)
    {
        _setDirection = setDirection;
        
        return this;
    }
    
    public CellBehaviour SetSprite(Sprite sprite)
    {
        _renderer.sprite = sprite;
        
        return this;
    }
    
    public CellBehaviour SetColor(Color color)
    {
        _renderer.color = color;
            
        return this;
    }
    
    public CellBehaviour SetSize(Vector2 size)
    {
        transform.localScale =size / _renderer.size;
        
        return this;
    }
    
    public CellBehaviour SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
        
        return this;
    }
    
    // todo on player drop cell
    public void SetDirection(Direction dir) => _setDirection?.Invoke(dir);
}
}
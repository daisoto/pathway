using System;
using UnityEngine;

namespace Gameplay
{
public class CellBehaviour: MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _renderer;
    
    private Action _onEnter;
    private Action<Direction> _setDirection;
    
    public CellBehaviour SetOnEnter(Action onEnter)
    {
        _onEnter = onEnter;
        
        return this;
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
    
    public CellBehaviour SetSize(Vector2 size)
    {
        transform.localScale =size / _renderer.size;
        
        return this;
    }
    
    public CellBehaviour SetPosition(Vector3 position)
    {
        transform.position = position;
        
        return this;
    }
    
    public CellBehaviour SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
        
        return this;
    }
    
    public void SetDirection(Direction dir) => _setDirection?.Invoke(dir);
    
    public void Enter() => _onEnter?.Invoke();
}
}
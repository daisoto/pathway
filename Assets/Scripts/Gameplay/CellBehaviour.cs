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
    
    public Vector3 Position { set => transform.position = value; }
    
    public Quaternion Rotation { set => transform.rotation = value; }

    public Vector3 Scale { set => transform.localScale = value; }
    
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
    
    public void SetDirection(Direction dir) => _setDirection?.Invoke(dir);
    
    public void Enter() => _onEnter?.Invoke();
}
}
using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
public class CellSetter: MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private Image _image;
    
    private Action<Vector2> _onEndDrag;
    
    private Transform _parent;
    private int _index;
    
    private void Awake()
    {
        _parent = transform.parent;
        _index = transform.GetSiblingIndex();
    }

    public CellSetter SetSprite(Sprite sprite)
    {
        _image.sprite = sprite;
        
        return this;
    }
    
    public CellSetter SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
        
        return this;
    }
    
    public CellSetter SetOnEndDrag(Action<Vector2> onEndDrag)
    {
        _onEndDrag = onEndDrag;
        
        return this;
    }

    public void OnDrag(PointerEventData eventData) =>
        transform.position = eventData.position;

    public void OnEndDrag(PointerEventData eventData) 
    {
        _onEndDrag?.Invoke(eventData.position);
        
        ResetPosition();
    }
    
    private void ResetPosition()
    {
        transform.SetParent(null, false);
        transform.SetParent(_parent, false);
        transform.SetSiblingIndex(_index);
    }
}
}
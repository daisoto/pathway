using System;
using UnityEngine;

namespace UI
{
public class CellSetterViewModel
{
    public Sprite Sprite { get; }
    public Quaternion Rotation { get; }
    public Action<Vector2> OnEndDrag { get; }
    
    public CellSetterViewModel(Sprite sprite, Quaternion rotation, 
        Action<Vector2> onEndDrag)
    {
        Sprite = sprite;
        Rotation = rotation;
        OnEndDrag = onEndDrag;
    }
}
}
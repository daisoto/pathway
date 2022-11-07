using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
public class CameraOperator: MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    
    [SerializeField]
    private GridBehaviour _gridBehaviour;
    
    private float _depth;
    
    private void Awake() =>
        _depth = _camera.transform.position.y - _gridBehaviour.Position.y;

    public Vector3 GetPointFromScreen(Vector2 pos) => 
        _camera.ScreenToWorldPoint(new Vector3(pos.x, pos.y, _depth));

    public IList<RaycastHit> Raycast(Vector2 pos)
    {
        var ray = _camera.ScreenPointToRay(pos);

        return Physics.RaycastAll(ray);
    }
}
}
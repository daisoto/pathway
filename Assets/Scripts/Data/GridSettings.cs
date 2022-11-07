using UnityEngine;

namespace Data
{
[CreateAssetMenu(fileName = "New GridSettings", menuName = "Grid settings")]
public class GridSettings: ScriptableObject
{
    [SerializeField]
    private Vector2Int _size;
    public Vector2Int Size => _size;
}
}
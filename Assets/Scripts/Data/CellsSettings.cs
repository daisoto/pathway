using Gameplay;
using UnityEngine;

namespace Data
{
[CreateAssetMenu(fileName = "New CellsSettings", menuName = "Cells settings")]
public class CellsSettings: ScriptableObject
{
    [SerializeField]
    private Sprite _directionSprite;
    
    [SerializeField]
    private Sprite _noDirectionSprite;
    
    [SerializeField]
    private Sprite _destinationSprite;
    public Sprite DestinationSprite => _destinationSprite;
    
    [SerializeField]
    private Color _defaultColor;
    public Color DefaultColor => _defaultColor;
    
    public Sprite GetSprite(Direction direction) =>
        direction == Direction.None ? _noDirectionSprite : _directionSprite;

    public Quaternion GetRotation(Direction direction)
    {
        switch (direction)
        {
            case Direction.Down:
                return Quaternion.AngleAxis(-90, Vector3.forward);
            case Direction.Up:
                return Quaternion.AngleAxis(90, Vector3.forward);
            case Direction.Left:
                return Quaternion.AngleAxis(0, Vector3.forward);
            case Direction.Right:
                return Quaternion.AngleAxis(180, Vector3.forward);
        }
        
        return Quaternion.identity;
    }
}
}
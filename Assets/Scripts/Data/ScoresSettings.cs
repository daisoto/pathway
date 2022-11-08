using UnityEngine;

namespace Data
{
[CreateAssetMenu(fileName = "New ScoresSettings", menuName = "Scores settings")]
public class ScoresSettings: ScriptableObject
{
    [SerializeField]
    private int _scoresForLevel;
    public int ScoresForLevel => _scoresForLevel;
}
}
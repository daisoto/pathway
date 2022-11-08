using UnityEngine;
using UnityEngine.UI;

namespace UI
{
public class ScoreView: View
{
    [SerializeField]
    private Text _score;
    
    public void SetScore(int score) => _score.text = score.ToString();
}
}
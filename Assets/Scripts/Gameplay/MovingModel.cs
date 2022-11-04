using UniRx;

namespace Gameplay
{
public class MovingModel
{
    public IReadOnlyReactiveProperty<bool> IsFinished => _isFinished;
    private readonly ReactiveProperty<bool> _isFinished;

    private Cell _currentCell;
    private readonly Cell _finalCell;
    
    public MovingModel(Cell initialCell, Cell finalCell)
    {
        _currentCell = initialCell;
        _finalCell = finalCell;
        _isFinished = new ReactiveProperty<bool>(false);
    }
    
    public void Move(Cell nextCell)
    {
        _currentCell = nextCell;
        if (_currentCell == _finalCell)
            _isFinished.Value = true;
    }
}
}
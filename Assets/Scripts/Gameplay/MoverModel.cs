namespace Gameplay
{
public class MoverModel
{
    public Cell CurrentCell
    {
        get => _currentCell;
        set
        {
            _currentCell = value;
            
            if (value == _finalCell)
                IsFinished = true;
        }
    }
    private Cell _currentCell;

    public bool IsFinished { get; private set; }

    private readonly Cell _finalCell;
    private readonly Cell _initialCell;
    
    public MoverModel(Cell initialCell, Cell finalCell)
    {
        _initialCell = initialCell;
        _finalCell = finalCell;
        
        _currentCell = initialCell;
        IsFinished = false;
    }
    
    public void Reset() => _currentCell = _initialCell;
}
}
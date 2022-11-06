using System;
using UniRx;
using UnityEngine;

namespace Gameplay
{
public class MoverModel
{
    public Color Color { get; }
    public float Speed { get; }
    
    public Cell CurrentCell
    {
        get => _currentCell;
        set
        {
            _currentCell = value;
            
            if (value == _finalCell)
                _isFinished.Value = true;
        }
    }
    private Cell _currentCell;
    
    public IObservable<Cell> ImmediateMove => _immediateMove; 
    private readonly ReactiveCommand<Cell> _immediateMove;
    
    public readonly ReactiveCommand<Cell> Move;

    public IReadOnlyReactiveProperty<bool> IsFinished => _isFinished;
    private readonly ReactiveProperty<bool> _isFinished; 

    private readonly Cell _finalCell;
    private readonly Cell _initialCell;
    
    public MoverModel(Cell initialCell, Cell finalCell, 
        Color color, float speed)
    {
        _initialCell = initialCell;
        _finalCell = finalCell;
        Color = color;
        Speed = speed;

        _currentCell = initialCell;
        _isFinished = new ReactiveProperty<bool>(false);
        Move = new ReactiveCommand<Cell>();
        _immediateMove = new ReactiveCommand<Cell>();
    }
    
    public void Reset()
    {
        _isFinished.Value = false;
        _currentCell = _initialCell;
        _immediateMove.Execute(_initialCell);
    }
}
}
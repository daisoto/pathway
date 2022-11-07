using System;
using Cysharp.Threading.Tasks;
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
    
    public IObservable<Cell> OnImmediateMove => _onImmediateMove; 
    private readonly ReactiveCommand<Cell> _onImmediateMove;
    
    public IObservable<Cell> OnMove => _onMove;
    private readonly ReactiveCommand<Cell> _onMove;

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
        _onMove = new ReactiveCommand<Cell>();
        _onImmediateMove = new ReactiveCommand<Cell>();
    }
    
    public async UniTask Move(Cell nextCell)
    {
        _onMove.Execute(nextCell);
        await UniTask.WaitUntil(() => CurrentCell == nextCell);
    }
    
    public void Reset()
    {
        _isFinished.Value = false;
        _currentCell = _initialCell;
        _onImmediateMove.Execute(_initialCell);
    }
}
}
using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay
{
public class MoverController
{
    private readonly MoverModel _model;
    private readonly MoverBehaviour _behaviour;
    
    private readonly Func<Cell, Cell> _nextCellProvider;
    private readonly Func<Cell, Vector3> _cellPositionProvider;

    public MoverController(MoverModel model, MoverBehaviour behaviour, 
        Func<Cell, Cell> nextCellProvider, 
        Func<Cell, Vector3> cellPositionProvider)
    {
        _model = model;
        _behaviour = behaviour;
        _nextCellProvider = nextCellProvider;
        _cellPositionProvider = cellPositionProvider;
    }
    
    public void StartMoving() => Move().Forget();
    
    private async UniTask Move()
    {
        while (!_model.IsFinished)
            await MoveInternal();
    }

    private async UniTask MoveInternal()
    {
        var currentCell = _model.CurrentCell;
        var nextCell = _nextCellProvider.Invoke(currentCell);
        
        if (currentCell == nextCell)
            return;
        
        var position = _cellPositionProvider.Invoke(nextCell);
        
        await _behaviour.Move(position);
        _model.CurrentCell = nextCell;
    }
    
    public void Reset()
    {
        _model.Reset();
        _behaviour
            .StopMoving()
            .SetPosition(
                _cellPositionProvider.Invoke(_model.CurrentCell));
    }
}
}
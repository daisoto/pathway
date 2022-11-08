using UniRx;
using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Gameplay
{
public class MoverController: IInitializable, IDisposable
{
    private readonly MoverModel _model;
    private readonly MoverBehaviour _behaviour;
    
    private readonly Func<Cell, Vector3> _cellPositionProvider;
    
    private readonly DisposablesContainer _disposablesContainer;

    public MoverController(MoverModel model, MoverBehaviour behaviour,
        Func<Cell, Vector3> cellPositionProvider)
    {
        _model = model;
        _behaviour = behaviour;
        _cellPositionProvider = cellPositionProvider;
        
        _disposablesContainer = new DisposablesContainer();
    }

    public void Initialize() => Bind();

    public void Dispose() => _disposablesContainer.Dispose();
    
    private void Bind()
    { 
        _behaviour
            .SetColor(_model.Color)
            .SetTimeToMove(1 / _model.Speed);
        
        _disposablesContainer.Add(_model.OnMove
            .Subscribe(nextCell => Move(nextCell).Forget()));
        
        _disposablesContainer.Add(_model.OnImmediateMove
            .Subscribe(Reset));
    }
    
    private async UniTask Move(Cell nextCell)
    {
        if (nextCell == _model.CurrentCell)
            return;
        
        await _behaviour.Move(_cellPositionProvider.Invoke(nextCell));
        _model.CurrentCell = nextCell;
    }
    
    private void Reset(Cell initialCell)
    {
        _behaviour
            .StopMoving()
            .SetPosition(_cellPositionProvider.Invoke(initialCell));
    }
}
}
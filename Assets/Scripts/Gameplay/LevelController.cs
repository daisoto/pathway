using UniRx;
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Gameplay
{
public class LevelController: IInitializable, IDisposable
{
    private readonly GridController _gridController;
    private readonly MoversManager _moversManager;
    private readonly SignalBus _signalBus; 
    
    private readonly DisposablesContainer _disposablesContainer;
    
    private List<MoverModel> _activeMovers;
    private bool _isPlaying;

    public LevelController(GridController gridController, 
        MoversManager moversManager, SignalBus signalBus)
    {
        _gridController = gridController;
        _moversManager = moversManager;
        _signalBus = signalBus;

        _disposablesContainer = new DisposablesContainer();
    }
    
    public void Initialize()
    {
        _gridController.CreateGrid();
        _moversManager.ResetMovers();
    }

    public void Dispose() => _disposablesContainer.Dispose();
    
    public void StartLevel()
    {
        Dispose();
        _activeMovers = new List<MoverModel>(_moversManager.Models);
        SubscribeVictory();

        _isPlaying = true;
        Move();
    }
    
    public void Reset()
    {
        _isPlaying = false;
        _gridController.ClearCells();
        _moversManager.ResetMovers();
    }
    
    private void Move()
    {
        foreach (var mover in _activeMovers)
            MoveInternal(mover).Forget();
    }

    private async UniTask MoveInternal(MoverModel model)
    {
        while (!model.IsFinished.Value && _isPlaying)
        {
            var nextCell = _gridController.GetNextCell(model.CurrentCell);
            await model.Move(nextCell);
        }
    }
    
    private void SubscribeVictory()
    {
        foreach (var mover in _activeMovers)
        {
            _disposablesContainer.Add(mover.IsFinished
                .Subscribe(isFinished =>
                {
                    if (isFinished)
                    {
                        _activeMovers.Remove(mover);
                        CheckVictory();
                    } 
                }));
        }
    }
    
    private void CheckVictory()
    {
        if (_activeMovers.Count == 0)
            _signalBus.Fire(new VictorySignal());
    }
}
}
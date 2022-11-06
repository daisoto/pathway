using UniRx;
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Gameplay
{
public class LevelController: IDisposable
{
    private readonly GridController _gridController;
    private readonly MoversManager _moversManager;
    
    private readonly DisposablesContainer _disposablesContainer;
    
    private List<MoverModel> _activeMovers;

    public LevelController(GridController gridController, 
        MoversManager moversManager)
    {
        _gridController = gridController;
        _moversManager = moversManager;
         
        _disposablesContainer = new DisposablesContainer();
    }

    public void Dispose() => _disposablesContainer.Dispose();
    
    public void StartLevel()
    {
        _gridController.CreateGrid();
        _moversManager.CreateMovers();
        
        _activeMovers = new List<MoverModel>(_moversManager.Models);
        
        SubscribeVictory();
    }
    
    public void Reset()
    {
        foreach (var mover in _moversManager.Models)
            mover.Reset();
    }
    
    public void Move()
    {
        foreach (var mover in _activeMovers)
            MoveInternal(mover).Forget();
    }

    private async UniTask MoveInternal(MoverModel model)
    {
        while (!model.IsFinished.Value)
        {
            var nextCell = _gridController.GetNextCell(model.CurrentCell);
            model.Move.Execute(nextCell);
            await UniTask.WaitUntil(() => model.CurrentCell == nextCell);
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
                        _activeMovers.Remove(mover);
                    CheckVictory();
                }));
        }
    }
    
    private void CheckVictory()
    {
        if (_activeMovers.Count == 0)
        {
            // todo victory
        }
    }
}
}
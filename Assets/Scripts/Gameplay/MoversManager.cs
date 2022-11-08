using System;
using System.Collections.Generic;
using Data;
using Zenject;

namespace Gameplay
{
public class MoversManager: IInitializable, IDisposable
{
    private readonly GridController _gridController;
    private readonly MoversSettings _settings;
    
    public IList<MoverModel> Models => _models;
    private readonly List<MoverModel> _models;
    
    private readonly DisposablesContainer _disposablesContainer;
    

    public MoversManager(MoversSettings settings, 
        GridController gridController)
    {
        _settings = settings;
        _gridController = gridController;

        _models = new List<MoverModel>();
        _disposablesContainer = new DisposablesContainer();
    }

    public void Initialize() => CreateMovers();
    
    public void Dispose() => _disposablesContainer.Dispose();
    
    private void CreateMovers()
    {
        foreach (var data in _settings.MoversData)
        {
            var model = new MoverModel(data.GetDistance, 
                    data.Color, data.Speed);
            
            _models.Add(model);
            
            var behaviour = _settings.GetMoverBehaviour();
            var controller = new MoverController(
                model, behaviour, _gridController.GetCellPosition);
            controller.Initialize();
            
            _disposablesContainer.Add(controller);
        }
    }
    
    public void ResetMovers()
    {
        _gridController.ClearOccupiedCells();
        foreach (var model in _models)
        {
            var (initialCell, finalCell) = _gridController
                .GetFiniteCells(model.DistanceProvider.Invoke);
            
            _gridController.MarkDestination(finalCell, model.Color);
                
            model
                .SetInitialCell(initialCell)
                .SetFinalCell(finalCell)
                .Reset();
        }
    }
}
}
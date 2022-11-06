using System;
using System.Collections.Generic;
using Data;

namespace Gameplay
{
public class MoversManager: IDisposable
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
    
    public void Dispose() => _disposablesContainer.Dispose();
    
    public void CreateMovers()
    {
        foreach (var data in _settings.MoversData)
        {
            var distance = data.GetDistance();
            var (initialCell, finalCell) = _gridController
                .GetFiniteCells(distance);
            
            _gridController.MarkDestination(finalCell, 
                data.DestinationSprite, data.Color);
            
            var model = new MoverModel(initialCell, finalCell, 
                data.Color, data.Speed);
            _models.Add(model);
            
            var behaviour = _settings.GetMoverBehaviour();
            var mover = new MoverController(
                model, behaviour, _gridController.GetCellPosition);
            mover.Initialize();
            
            _disposablesContainer.Add(mover);
        }
    }
}
}
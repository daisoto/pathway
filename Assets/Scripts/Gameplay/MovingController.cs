using Data;
using UnityEngine;

namespace Gameplay
{
public class MovingController
{
    private readonly GridController _gridController;
    private readonly MovingData _data;
    private readonly MovingBehaviour _behaviour;
    
    private MovingModel _model;

    public MovingController(MovingData data, MovingBehaviour behaviour, GridController gridController)
    {
        _data = data;
        _behaviour = behaviour;
        _gridController = gridController;
    }
    
    public void InitModel()
    {
        var distance = _data.GetDistance();
        var startingCell = _gridController.GetRandomCell();
        var finalCell = _gridController
            .GetEquidistantCell(startingCell.Index, distance);
        // todo надо сравнивать с занятыми клетками поэтому лучше это вынести в общий контроллер 
        // как и все операции с GridController
        _model = new MovingModel(startingCell, finalCell);
    }
}
}
using System.Collections.Generic;
using Data;
using Zenject;

namespace Gameplay
{
public class MoversManager: IInitializable
{
    private readonly GridController _gridController;
    private readonly MoversSettings _settings;
    
    private readonly List<MoverController> _movers;
    private readonly List<Cell> _occupiedCells;

    public MoversManager(MoversSettings settings, 
        GridController gridController)
    {
        _settings = settings;
        _gridController = gridController;
        
        _occupiedCells = new List<Cell>();
        _movers = new List<MoverController>();
        
        _gridController.CreateGrid();
    }

    public void Initialize() => CreateMovers();
    
    public void StartMoving() => 
        _movers.ForEach(m => m.StartMoving()); 
    
    public void Reset() => 
        _movers.ForEach(m => m.Reset()); 
    
    private void CreateMovers()
    {
        foreach (var data in _settings.MoversData)
        {
            var distance = data.GetDistance();
            var (initialCell, finalCell) = GetFiniteCells(distance);
            
            _gridController.MarkDestination(finalCell, 
                data.DestinationSprite, data.Color);
            
            var model = new MoverModel(initialCell, finalCell);
            var behaviour = _settings
                .GetMoverBehaviour()
                .SetColor(data.Color)
                .SetTimeToMove(1 / data.Speed)
                .SetPosition(_gridController.GetCellPosition(initialCell));
            
            _movers.Add(
                new MoverController(
                    model, behaviour, 
                    _gridController.GetNextCell, 
                    _gridController.GetCellPosition));
        }
    }

    private (Cell, Cell) GetFiniteCells(int distance)
    {
        var (initialCell, finalCell) = GetFiniteCellsInternal(distance);
            
        while (_occupiedCells.Contains(initialCell) ||
               _occupiedCells.Contains(finalCell))
            (initialCell, finalCell) = GetFiniteCellsInternal(distance);
            
        _occupiedCells.Add(initialCell);
        _occupiedCells.Add(finalCell);
        
        return (initialCell, finalCell);
    }
    
    private (Cell, Cell) GetFiniteCellsInternal(int distance)
    {
        var startingCell = _gridController.GetRandomCell();
        var finalCell = _gridController
            .GetEquidistantCell(startingCell.Index, distance);
        
        return (startingCell, finalCell);
    }
}
}
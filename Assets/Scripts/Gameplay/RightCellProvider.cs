namespace Gameplay
{ 
public class RightCellProvider: NextCellProvider
{
    public RightCellProvider(Cell[,] cells) : base(cells) { }

    public override Cell GetNextCell(Cell initialCell)
    {
        var nextX = initialCell.Position.x + 1;
        
        return nextX < _cells.GetLength(0) ? 
            _cells[nextX, initialCell.Position.y] : initialCell;
    }
}
}
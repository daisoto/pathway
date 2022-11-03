namespace Gameplay
{
public class LeftCellProvider:  NextCellProvider
{
    public LeftCellProvider(Cell[,] cells) : base(cells) { }

    public override Cell GetNextCell(Cell initialCell)
    {
        var nextX = initialCell.Position.x - 1;
        
        return nextX > 0 ? _cells[nextX, initialCell.Position.y] : initialCell;
    }
}
}
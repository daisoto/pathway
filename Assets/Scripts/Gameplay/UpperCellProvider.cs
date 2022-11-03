namespace Gameplay
{
public class UpperCellProvider: NextCellProvider
{
    public UpperCellProvider(Cell[,] cells) : base(cells) { }

    public override Cell GetNextCell(Cell initialCell)
    {
        var nextY = initialCell.Position.y - 1;
        
        return nextY > 0 ? _cells[initialCell.Position.x, nextY] : initialCell;
    }
}
}
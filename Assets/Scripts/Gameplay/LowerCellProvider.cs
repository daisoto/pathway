namespace Gameplay
{
public class LowerCellProvider: NextCellProvider
{
    public LowerCellProvider(Cell[,] cells) : base(cells) { }

    public override Cell GetNextCell(Cell initialCell)
    {
        var nextY = initialCell.Position.y + 1;
        
        return nextY < _cells.GetLength(1) ? 
            _cells[initialCell.Position.x, nextY] : initialCell;
    }
}
}
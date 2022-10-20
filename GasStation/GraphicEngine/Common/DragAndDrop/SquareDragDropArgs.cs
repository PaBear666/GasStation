namespace GasStation.GraphicEngine.Common
{
    public class SquareDragDropArgs<D,S> : SquareArgs<S>
        where D: class
        where S: Square
    {
        public DragAndDropData<D> Data { get; set; }
        public SquareDragDropArgs(S square, DragAndDropData<D> data) : base(square)
        {
            Data = data;
            Square = square;
        }
    }
}

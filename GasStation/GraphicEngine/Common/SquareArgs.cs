using System;

namespace GasStation.GraphicEngine.Common
{
    public class SquareArgs <T> : EventArgs
        where T : Square
    {
        public T Square;
        public SquareArgs(T square) : base()
        {
            Square = square;    
        }
    }

    public class DragSquareArgs<T> : SquareArgs<T>
    where T : Square
    {
        public T DataSquare;
        public DragSquareArgs(T dataSquare, T senderSquare) : base(senderSquare)
        {
            DataSquare = dataSquare;
        }
    }
}

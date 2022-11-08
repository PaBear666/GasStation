using System;

namespace GasStation.GraphicEngine.Common
{
    public class SquareArgs <S> : EventArgs
        where S : Square
    {
        public S Square { get; set; }
        public SquareArgs(S square) : base()
        {
            Square = square;    
        }
    }
}

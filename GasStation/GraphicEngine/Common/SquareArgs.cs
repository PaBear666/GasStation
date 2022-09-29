using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}

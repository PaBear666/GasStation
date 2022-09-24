using GasStation.GraphicEngine.Common;
using System.Drawing;
using System.Windows.Forms;

namespace GasStation.GraphicEngine
{
    class ConstructorArea : Area<ColorSquare>
    {
        public ConstructorArea(Panel panel, Size squareSize) : base(panel, squareSize)
        {
            for (int i = 0; i < SquareWidthLength; i++)
            {
                for (int j = 0; j < SquareHeightLength; j++)
                {
                    var square = new ColorSquare(new Point(i * squareSize.Width, j * squareSize.Height), squareSize, Color.Gray);
                    AddSquare(square, i, j);
                }
            }
        }
    }
}

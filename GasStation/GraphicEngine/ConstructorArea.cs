using GasStation.GraphicEngine.Common;
using System.Drawing;
using System.Windows.Forms;

namespace GasStation.GraphicEngine
{
    class ConstructorArea : Area<ColorSquare>
    {
        public ConstructorArea(Panel panel, Size squareSize) : base(panel, squareSize)
        {
            int id = 1;
            for (int i = 0; i < SquareWidthLength; i++)
            {
                for (int j = 0; j < SquareHeightLength; j++)
                {
                    var square = new ColorSquare(id, new Point(i * squareSize.Width, j * squareSize.Height), squareSize, Color.Gray);
                    AddSquare(square, i, j);
                    id++;
                }
            }
        }
    }
}

using System.Drawing;
using System.Windows.Forms;

namespace GasStation.GraphicEngine.Common
{
    public class Area
    {
        readonly Panel _panel;
        public Square[,] Squares { get; }
        public Size SquareSize { get; }

        public Area(Panel panel, Size squareSize)
        {
            _panel = panel;
            SquareSize = squareSize;

            int squareWidthLength = _panel.Width / SquareSize.Width;
            int squareHeightLength = _panel.Height / SquareSize.Height;
            Squares = new Square[squareWidthLength, squareHeightLength];

            for (int i = 0; i < squareWidthLength; i++)
            {
                for (int j = 0; j < squareHeightLength; j++)
                {
                    var square = new Square(new Point(i * SquareSize.Width, j * SquareSize.Height), SquareSize);
                    _panel.Controls.Add(square.Control);
                    Squares[i, j] = square;
                    square.Control.BackColor = Color.Red;
                }
            }
        }
    }
}

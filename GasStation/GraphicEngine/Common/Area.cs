using System.Drawing;
using System.Windows.Forms;

namespace GasStation.GraphicEngine.Common
{
    public abstract class Area <S>
        where S : Square
    {
        private readonly Panel _panel;
        public int SquareWidthLength { get; }
        public int SquareHeightLength { get; }
        private S[,] _squares { get; }
        public Size SquareSize { get; }

        public Area(Panel panel, Size squareSize)
        {
            _panel = panel;
            SquareSize = squareSize;
            SquareWidthLength = _panel.Width / squareSize.Width;
            SquareHeightLength = _panel.Height / squareSize.Height;

            _squares = new S[SquareWidthLength, SquareHeightLength];
        }

        protected virtual void AddSquare(S square, int widthIndex, int heightIndex)
        {
            _squares[widthIndex, heightIndex] = square;
            _panel.Controls.Add(square.Control);
        }

        public S GetSquare(int widthIndex, int heightIndex)
        {
            return _squares[widthIndex, heightIndex];
        }
    }
}

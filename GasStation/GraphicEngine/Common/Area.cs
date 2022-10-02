using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.Remoting.Channels;
using System.Windows.Forms;

namespace GasStation.GraphicEngine.Common
{
    public abstract class Area <S>
        where S : Square
    {
        private readonly Panel _panel;
        public event EventHandler<SquareArgs<S>> ClickSquare;
        public event EventHandler<DragSquareArgs<S>> DragDropSquare;
        public event EventHandler<DragSquareArgs<S>> DragOverSquare;
        public event EventHandler<SquareArgs<S>> DragLeaveSquare;
        public int SquareWidthLength { get; }
        public int SquareHeightLength { get; }
        private S[] Squares { get; }
        public Size SquareSize { get; }

        public Area(Panel panel, Size squareSize, int length)
        {
            _panel = panel;
            SquareSize = squareSize;
            SquareWidthLength = _panel.Width / squareSize.Width;
            SquareHeightLength = _panel.Height / squareSize.Height;

            Squares = new S[length * length];
        }

        protected void AddSquare(int index, S square)
        {
            var currentSquare = Squares[index];
            if(currentSquare != null)
            {
                _panel.Controls.Remove(currentSquare.Control);
            }

            Squares[index] = square;
            _panel.Controls.Add(square.Control);
            square.Control.AllowDrop = true;

            square.Control.MouseDown += (sender, args) =>
            {
                ClickSquare.Invoke(this, new SquareArgs<S>(square));
                square.Control.DoDragDrop(square, DragDropEffects.All);
            };
            
            square.Control.DragDrop += (object sender, DragEventArgs e) =>
            {
                var dataSquare = e.Data.GetData(typeof(S)) as S;
                DragDropSquare.Invoke(this, new DragSquareArgs<S>(dataSquare, square));

            };

            square.Control.DragOver += (object sender, DragEventArgs e) =>
            {
                e.Effect = DragDropEffects.Move;
                var dataSquare = e.Data.GetData(typeof(S)) as S;
                DragOverSquare.Invoke(this, new DragSquareArgs<S>(dataSquare, square));
            };

            square.Control.DragLeave += (object sender, EventArgs e) =>
            {
                DragLeaveSquare.Invoke(this, new SquareArgs<S>(square));
            };

        }

        public S GetSquare(int index)
        {
            return Squares[index];
        }

        public S[] GetAllSquares()
        {
            return Squares;
        }
    }
}

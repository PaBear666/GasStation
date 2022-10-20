using GasStation.LifeEngine;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace GasStation.GraphicEngine.Common
{
    public abstract class Area <S,D>
        where S : Square
        where D : class
    {
        private readonly Panel _panel;
        private readonly int _squareLength;
        public event EventHandler<SquareArgs<S>> MouseDownSquare;
        public event EventHandler<SquareDragDropArgs<D,S>> DragDropSquare;
        public event EventHandler<SquareDragDropArgs<D,S>> DragOverSquare;
        public event EventHandler<SquareArgs<S>> DragLeaveSquare;
        public int SquareWidthLength { get; }
        public int SquareHeightLength { get; }
        private S[,] Squares { get; }
        public Size SquareSize { get; }

        public Area(Panel panel, Size squareSize, int length)
        {
            _panel = panel;
            SquareSize = squareSize;
            SquareWidthLength = _panel.Width / squareSize.Width;
            SquareHeightLength = _panel.Height / squareSize.Height;
            _squareLength = length;
            Squares = new S[length,length];
        }

        protected void AddSquare(int index, S square)
        {
            var currentSquare = GetSquare(index);
            if (currentSquare != null)
            {
                _panel.Controls.Remove(currentSquare.Control);
            }

            Squares[index / _squareLength, index % _squareLength] = square;
            _panel.Controls.Add(square.Control);
            square.Control.AllowDrop = true;


            square.Control.MouseDown += (sender, args) =>
            {
                MouseDownSquare.Invoke(this, new SquareArgs<S>(square));
            };
            
            square.Control.DragDrop += (object sender, DragEventArgs e) =>
            {
                var dataSquare = e.Data.GetData(typeof(DragAndDropData<D>)) as DragAndDropData<D>;
                DragDropSquare.Invoke(this, new SquareDragDropArgs<D,S>(square, dataSquare));
            };

            square.Control.DragOver += (object sender, DragEventArgs e) =>
            {
                e.Effect = DragDropEffects.Move;
                var dataSquare = e.Data.GetData(typeof(DragAndDropData<D>)) as DragAndDropData<D>;
                DragOverSquare.Invoke(this, new SquareDragDropArgs<D, S>(square, dataSquare));
            };

            square.Control.DragLeave += (object sender, EventArgs e) =>
            {
                DragLeaveSquare.Invoke(this, new SquareArgs<S>(square));
            };

        }

        public S GetSquare(int index)
        {
            return Squares[index / _squareLength, index % _squareLength];
        }

        public S[] GetAroundSquares(int index)
        {
            int width = index / _squareLength;
            int height = index % _squareLength;
            var squares = new S[9];

            int k = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                { 
                    if((width + i) >= _squareLength || (height + j) >= _squareLength || (width + i) < 0 || (height + j) < 0)
                    {
                        squares[k] = null;
                    }
                    else
                    {
                        squares[k] = Squares[width + i, height + j];
                    }
                    
                }
            }

            return squares;
        }
    }
}

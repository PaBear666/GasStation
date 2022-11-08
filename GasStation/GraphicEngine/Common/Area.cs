using GasStation.ConstructorEngine;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using System.Security.Cryptography;
using System.Reflection;

namespace GasStation.GraphicEngine.Common
{
    public abstract class Area <S,D> : IDisposable
        where S : Square
        where D : class
    {
        private readonly Panel _panel;
        public event EventHandler<SquareArgs<S>> MouseLeftDownSquare;
        public event EventHandler<SquareArgs<S>> MouseMiddleDownSquare;
        public event EventHandler<SquareArgs<S>> MouseRightDownSquare;
        public event EventHandler<SquareDragDropArgs<D,S>> SuccessDragDropSquare;
        public event EventHandler<SquareDragDropArgs<D,S>> DragOverSquare;
        public event EventHandler<SquareDragDropArgs<D,S>> DragEnterSquare;
        public event EventHandler<EventArgs> EndDragDrop;
        public event EventHandler<SquareArgs<S>> DragLeaveSquare;
        public int WidthLength { get; }
        public int Heightength { get; }
        protected S[] Squares { get; }
        public Size SquareSize { get; }

        public Area(Panel panel, int widthLength, int heightLength)
        {
            _panel = panel;
            SquareSize = new Size(_panel.Width / widthLength, _panel.Height / heightLength);
            WidthLength = widthLength;
            Heightength = heightLength;
            Squares = new S[widthLength * heightLength];
        }

        protected void AddSquare(int index, S square)
        {
            var currentSquare = GetSquare(index);
            if (currentSquare != null)
            {
                _panel.Controls.Remove(currentSquare.Control);
            }

            Squares[index] = square;
            _panel.Controls.Add(square.Control);
            square.Control.AllowDrop = true;


            square.Control.MouseDown += (sender, args) =>
            {
                switch (args.Button)
                {
                    case MouseButtons.Left:
                        MouseLeftDownSquare?.Invoke(this, new SquareArgs<S>(square));
                        break;
                    case MouseButtons.Right:
                        MouseRightDownSquare?.Invoke(this, new SquareArgs<S>(square));
                        break;
                    case MouseButtons.Middle:
                        MouseMiddleDownSquare?.Invoke(this, new SquareArgs<S>(square));
                        break;
                }
               
            };
            
            square.Control.DragDrop += (object sender, DragEventArgs e) =>
            {
                var dataSquare = e.Data.GetData(typeof(DragAndDropData<D>)) as DragAndDropData<D>;
                SuccessDragDropSquare?.Invoke(this, new SquareDragDropArgs<D,S>(square, dataSquare));
            };

            square.Control.DragOver += (object sender, DragEventArgs e) =>
            {
                e.Effect = DragDropEffects.Move;
                var dataSquare = e.Data.GetData(typeof(DragAndDropData<D>)) as DragAndDropData<D>;
                DragOverSquare?.Invoke(this, new SquareDragDropArgs<D, S>(square, dataSquare));
            };

            square.Control.DragEnter += (object sender, DragEventArgs e) =>
            {
                var dataSquare = e.Data.GetData(typeof(DragAndDropData<D>)) as DragAndDropData<D>;
                DragEnterSquare?.Invoke(this, new SquareDragDropArgs<D, S>(square, dataSquare));
            };

            square.Control.DragLeave += (object sender, EventArgs e) =>
            {
                DragLeaveSquare?.Invoke(this, new SquareArgs<S>(square));
            };


            square.Control.QueryContinueDrag += (sender, args) =>
            {
                if(args.Action == DragAction.Drop)
                {
                    EndDragDrop?.Invoke(this, new SquareArgs<S>(square));
                }
            };

        }

        public S GetSquare(int index)
        {
            return Squares[index];
        }


        public S[] GetArroundSquares(Square square)
        {
            var squares = new S[9];
            var squareIdDes = square.Id / Heightength;
            int k = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    var index = square.Id + i * Heightength + j;
                    if(index < WidthLength * Heightength 
                        && index >= 0 
                        && index / Heightength - squareIdDes == i)
                    {
                        squares[k] = Squares[index];
                    }
                    else
                    {
                        squares[k] = null;
                    }

                    k++;
                }
            }
            return squares;
        }

        public void ForSquares(Action<S> action)
        {
            Squares.AsParallel().ForAll(action);
        }

        public virtual void Dispose()
        {
            foreach (var square in Squares)
            {
                square.Dispose();
            }
        }
    }
}

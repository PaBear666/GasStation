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
    public abstract class Area <S> : IDisposable
        where S : Square
    {
        private readonly Panel _panel;
        protected event EventHandler<SquareArgs<S>> MouseLeftDownSquare;
        protected event EventHandler<SquareArgs<S>> MouseMiddleDownSquare;
        protected event EventHandler<SquareArgs<S>> MouseRightDownSquare;
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

        protected virtual void AddSquare(int index, S square)
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
        }

        public S GetSquare(int index)
        {
            return Squares[index];
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

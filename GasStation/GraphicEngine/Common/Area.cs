﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GasStation.GraphicEngine.Common
{
    public abstract class Area <S>
        where S : Square
    {
        private readonly Panel _panel;
        public event EventHandler<SquareArgs<S>> ClickSquare;
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

            square.Control.Click += (sender, args) =>
            {
                ClickSquare(this, new SquareArgs<S>(square));
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

using GasStation.LifeEngine;
using System;

namespace GasStation.GraphicEngine.Common
{
    public class SquareArgs <T> : EventArgs
        where T : Square
    {
        public T Square { get; set; }
        public SquareArgs(T square) : base()
        {
            Square = square;    
        }
    }

    public class DragDropArgs<T> : EventArgs
    where T : class{
        public T DragDropElement { get; set; }
        public Action DragDropFinish { get; set; }
        public DragDropArgs(T dragDropElement, Action dragDropFinish)
        {
            DragDropElement = dragDropElement;
            DragDropFinish = dragDropFinish;
        }
    }

    public class SquareDragDropArgs<T,S> : DragDropArgs<T>
        where T: class
        where S: Square
    {
        public S Square { get; set; }
        public SquareDragDropArgs(S square, T dragDropElement, Action dragDropFinish) : base(dragDropElement, dragDropFinish)
        {
            Square = square;
        }
    }
}

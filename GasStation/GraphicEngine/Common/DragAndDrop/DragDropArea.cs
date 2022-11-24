using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GasStation.GraphicEngine.Common
{
    public abstract class DragDropArea <S,D> : Area<S>
        where S : Square
        where D : class
    {
        protected DragDropArea(Panel panel, int widthLength, int heightLength) : base(panel, widthLength, heightLength)
        {
        }

        public event EventHandler<SquareDragDropArgs<D, S>> SuccessDragDropSquare;
        public event EventHandler<SquareDragDropArgs<D, S>> DragOverSquare;
        public event EventHandler<SquareDragDropArgs<D, S>> DragEnterSquare;
        public event EventHandler<EventArgs> EndDragDrop;
        public event EventHandler<SquareArgs<S>> DragLeaveSquare;

        protected override void AddSquare(int index, S square)
        {
            base.AddSquare(index, square);


            square.Control.DragDrop += (object sender, DragEventArgs e) =>
            {
                var dataSquare = e.Data.GetData(typeof(DragAndDropData<D>)) as DragAndDropData<D>;
                SuccessDragDropSquare?.Invoke(this, new SquareDragDropArgs<D, S>(square, dataSquare));
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
                if (args.Action == DragAction.Drop)
                {
                    EndDragDrop?.Invoke(this, new SquareArgs<S>(square));
                }
            };
        }
    }
}

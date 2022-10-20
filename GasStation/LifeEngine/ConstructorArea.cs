using GasStation.GraphicEngine.Common;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GasStation.LifeEngine
{
    class ConstructorArea : Area<LifeSquare, Appliance>
    {
        public SurfaceEditor SurfaceEditor { get; }
        readonly SurfaceProvider _surfaceProvider;
        
        public ConstructorArea(Panel panel, SurfaceProvider surfaceProvider, int size, int length) : base(panel, new Size(size, size), length)
        {
            _surfaceProvider = surfaceProvider;

            SurfaceEditor = new SurfaceEditor(SurfaceType.Earth);
            InitArea(size, length);
            SurfaceEditor.ChooseSurface(SurfaceType.None);
        }      

        private void InitArea(int size, int length)
        {
            int id = 0;
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    var square = new LifeSquare(id, new Point(i * size, j * size), new Size(size, size), _surfaceProvider.GetSurface(SurfaceEditor.CurrentSurfase));
                    AddSquare(id, square);
                    id++;
                }
            }

            DragDropSquare += DropSquare;
            DragOverSquare += OverSquare;
            DragLeaveSquare += LeaveSquare;
            MouseDownSquare += DownMouse;
            MouseDownSquare += SetSurface;
        }

        private void DownMouse(object sender, SquareArgs<LifeSquare> e)
        {
            if(e.Square.Appliance != null)
            {
                e.Square.Control.DoDragDrop(new DragAndDropData<Appliance>(e.Square.Appliance, () => e.Square.Appliance = null), DragDropEffects.All);
            }           
        }

        private void LeaveSquare(object sender, SquareArgs<LifeSquare> e)
        {
            e.Square.ResetDesign();
        }

        private void SetSurface(object sender, SquareArgs<LifeSquare> e)
        {
            if(SurfaceEditor.CurrentSurfase != SurfaceType.None)
            {
                e.Square.Surface = _surfaceProvider.GetSurface(SurfaceEditor.CurrentSurfase);
            }
        }
        private void DropSquare(object sender, SquareDragDropArgs<Appliance, LifeSquare> e)
        {
            e.Square.Appliance = e.Data.DragDropComponent;
            e.Data.FinishDragDrop();
        }

        private void OverSquare(object sender, SquareDragDropArgs<Appliance, LifeSquare> e)
        {
            e.Square.SetDesign(e.Data.DragDropComponent.ViewComponent);
        }
    }
}

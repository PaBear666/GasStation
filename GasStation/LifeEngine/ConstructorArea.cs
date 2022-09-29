using GasStation.GraphicEngine.Common;
using GasStation.LifeEngine;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GasStation.LifeEngine
{
    class ConstructorArea : Area<LifeSquare>
    {
        public SurfaceSetuper SurfaceSetuper { get; }
        readonly SurfaceProvider _surfaceProvider;
        
        public ConstructorArea(Panel panel, int size, int length) : base(panel, new Size(size, size), length)
        {
            _surfaceProvider = new SurfaceProvider(new Surface[]{
                new Surface(SurfaceType.Earth, new ViewComponent(Color.Green)),
                new Surface(SurfaceType.GasStation, new ViewComponent(Color.Red))
            });

            SurfaceSetuper = new SurfaceSetuper(SurfaceType.Earth);
            InitArea(size, length);


            ClickSquare += ConstructorArea_ClickSquare;
        }

        private void ConstructorArea_ClickSquare(object sender, SquareArgs<LifeSquare> e)
        {
            e.Square.Surface = _surfaceProvider.GetSurface(SurfaceSetuper.CurrentSurfase);
        }        

        private void InitArea(int size, int length)
        {
            int id = 0;
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    var square = new LifeSquare(id, new Point(i * size, j * size), new Size(size, size), _surfaceProvider.GetSurface(SurfaceSetuper.CurrentSurfase));
                    AddSquare(id, square);
                    id++;
                }
            }
        }
    }
}

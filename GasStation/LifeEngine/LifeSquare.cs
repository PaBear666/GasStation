using GasStation.GraphicEngine;
using System.Drawing;

namespace GasStation.LifeEngine
{
    class LifeSquare : ColorSquare
    {
        Simulation _overEntity;
        Surface _surface;

        public LifeSquare(int id, Point location, Size size, Surface surface) 
            : base (id, location, size, surface.Color, surface.Image)
        {
            Surface = surface;
        }

        public Simulation OverEntity
        {
            get
            {
                return _overEntity;
            }

            set
            {
                SetDesign(value.Color);
                SetDesign(value.Image);
                _overEntity = value;
            }
        }
        public Surface Surface
        {
            get
            {
                return _surface;
            }

            set
            {
                BaseColor = value.Color;
                BaseImage = value.Image;
                _surface = value;
            }
        }
    }
}

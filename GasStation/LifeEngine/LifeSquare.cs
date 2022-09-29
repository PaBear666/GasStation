using GasStation.GraphicEngine;
using GasStation.GraphicEngine.Common;
using System;
using System.Drawing;

namespace GasStation.LifeEngine
{
    public class LifeSquare : ColorSquare
    {
        Appliance _overEntity;
        Surface _surface;

        public LifeSquare(int id, Point location, Size size, Surface surface) 
            : base (id, location, size, surface.ViewComponent)
        {
            Surface = surface;
        }

        
        public Appliance OverEntity
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
                BaseColor = value.ViewComponent.Color;
                BaseImage = value.ViewComponent.Image;
                _surface = value;
            }
        }
    }
}

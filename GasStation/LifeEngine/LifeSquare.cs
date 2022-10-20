using GasStation.GraphicEngine;
using System.Drawing;

namespace GasStation.LifeEngine
{
    public class LifeSquare : ColorSquare<Appliance>
    {
        Appliance _appliance;
        Surface _surface;

        public LifeSquare(int id, Point location, Size size, Surface surface) 
            : base (id, location, size, surface.ViewComponent)
        {
            Surface = surface;
        }

        
        public Appliance Appliance
        {
            get
            {
                return _appliance;
            }

            set
            {
                if(value == null)
                {
                    ResetDesign();
                    _appliance = value;
                    return;
                }

                SetDesign(value.ViewComponent);
                _appliance = value;
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
                BaseViewComponent = value.ViewComponent;
                _surface = value;
            }
        }

        public override void FinishDragDrop()
        {
            Appliance = null;
        }

        public override Appliance GetDragDropComponent()
        {
            return _appliance;
        }
    }
}

using GasStation.GraphicEngine;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace GasStation.LifeEngine
{
    public class LifeSquare : ColorSquare
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
                    _appliance = value;
                    ResetDesign();
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

        public override void ResetDesign()
        {
            if(Appliance != null)
            {
                _pictureBox.BackColor = Appliance.ViewComponent.Color;
                _pictureBox.Image = Appliance.ViewComponent.Image;
                return;
            }

            if(Surface != null)
            {
                _pictureBox.BackColor = Surface.ViewComponent.Color;
                _pictureBox.Image = Surface.ViewComponent.Image;
                return;
            }

            base.ResetDesign();
        }
    }
}

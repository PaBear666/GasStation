using GasStation.GraphicEngine;
using Newtonsoft.Json;
using System.Drawing;

namespace GasStation.LifeEngine
{
    [JsonObject(MemberSerialization.OptIn)]
    public class LifeSquare : ColorSquare
    {
        Appliance _appliance;
        Surface _surface;

        [JsonProperty]
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

        [JsonProperty]
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

        public LifeSquare(int id, Point location, Size size, Surface surface)
            : base(id, location, size, surface.ViewComponent)
        {
            Surface = surface;
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

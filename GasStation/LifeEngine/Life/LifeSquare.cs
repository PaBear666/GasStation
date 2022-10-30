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
        public Surface Surface
        {
            get
            {
                return _surface;
            }

            set
            {
                _surface = value;
                BaseBackgroundImage = value.ViewComponent.Image;
                BaseBackgroundColor = value.ViewComponent.Color;
                ResetDesign();
            }
        }

        [JsonProperty]
        public Appliance Appliance
        {
            get
            {
                return _appliance;
            }

            set
            {
                _appliance = value;
                if(value != null)
                {
                    BaseFrontImage = value.ViewComponent.Image;

                    if (value.ViewComponent.Image == null)
                    {
                        BaseBackgroundColor = value.ViewComponent.Color;
                        BaseBackgroundImage = null;
                    }
                }
                else
                {
                    BaseFrontImage = null;
                    BaseBackgroundColor = Surface.ViewComponent.Color;
                    BaseBackgroundImage = Surface.ViewComponent.Image;
                }

                ResetDesign();
            }
        }

        public void ShowAppliance()
        {
            SetFrontImage(Appliance?.ViewComponent.Image);
        }


        public void FillColor(Color color)
        {
            SetBackgroundColor(color);
            ShowAppliance();
            SetBackgroundImage(null);
        }

        public void HideAppliance()
        {
            SetFrontImage(null);
        }

        public LifeSquare(int id, Point location, Size size, Surface surface)
            : base(id, location, size)
        {
            Surface = surface;
        }
    }
}

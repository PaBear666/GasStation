using GasStation.GraphicEngine;
using GasStation.ConstructorEngine.Life;
using Newtonsoft.Json;
using System.Drawing;

namespace GasStation.ConstructorEngine
{
    public class LifeSquare : ColorSquare
    {
        LifeAppliance _appliance;
        Surface _surface;

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

        public LifeAppliance LifeAppliance
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
            SetFrontImage(LifeAppliance?.ViewComponent.Image);
        }


        public void FillColor(Color color)
        {
            SetBackgroundColor(color);
            SetBackgroundImage(null);
        }

        public void HideAppliance()
        {
            SetFrontImage(null);
        }
        
        public TransferSquare GetTransferSquare()
        {
           return new TransferSquare(Id, _surface, _appliance);
        }

        public LifeSquare(int id, Point location, Size size, Surface surface)
            : base(id, location, size)
        {
            Surface = surface;
        }
    }
}

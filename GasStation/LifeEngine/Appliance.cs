using GasStation.GraphicEngine.Common.Abstract;
using System.Drawing;

namespace GasStation.LifeEngine
{
    public abstract class Appliance
    {
        public ApplianceType Type { get; set; }

        public Image Image { get; set; }

        public Color Color { get; set; }

        public Appliance(ApplianceType type, Color color, Image image = null)
        {
            Type = type;
            Color = color;
            Image = image;
        }
    }
}
using System.Drawing;

namespace GasStation.LifeEngine
{
    public class Surface
    {
        public SurfaceType Type { get; set; }
        public Image Image { get; set; }
        public Color Color { get; set; }

        public Surface(SurfaceType type, Color color, Image image = null)
        {
            Type = type;
            Image = image;
            Color = color;
        }
    }
}
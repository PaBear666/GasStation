using System.Drawing;


namespace GasStation.GraphicEngine.Common
{
    public class ViewComponent
    {
        public Image Image { get; set; }
        public Color Color { get; set; }

        public ViewComponent(Color color, Image image = null)
        {
            Image = image;
            Color = color;
        }
    }
}

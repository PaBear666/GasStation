using GasStation.GraphicEngine.Common;
using GasStation.GraphicEngine.Common.Abstract;
using System.Drawing;

namespace GasStation.GraphicEngine
{
    public class ColorSquare : Square, IAnimatedComponent
    {
        public Color BaseColor { get; set; }
        public Image BaseImage { get; set; }

        public ColorSquare(Point location, Size size, Color baseColor) : base(location, size)
        {
            BaseColor = baseColor;
            SetDesign(baseColor);
        }

        public void ReturnBaseDesign()
        {
            _pictureBox.BackColor = BaseColor;
            _pictureBox.Image = BaseImage;
        }

        public void SetDesign(Color backColor)
        {
            _pictureBox.BackColor = backColor;
        }

        public void SetDesign(Image image)
        {
            _pictureBox.Image = image;
        }
    }
}

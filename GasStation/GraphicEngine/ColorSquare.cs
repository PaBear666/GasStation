using GasStation.GraphicEngine.Common;
using GasStation.GraphicEngine.Common.Abstract;
using System.Drawing;
using System.Windows.Forms;

namespace GasStation.GraphicEngine
{
    public class ColorSquare : Square, IAnimatedComponent
    {
        public Color BaseColor { get; set; }
        public Image BaseImage { get; set; }
        public ColorSquare(int id, Point location, Size size, Color baseColor, Image baseImage = null) : base(id, location, size)
        {
            _pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            BaseColor = baseColor;
            BaseImage = baseImage;
            SetDesign(baseImage);
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

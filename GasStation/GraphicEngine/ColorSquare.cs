using GasStation.GraphicEngine.Common;
using GasStation.GraphicEngine.Common.Abstract;
using System.Drawing;
using System.Windows.Forms;

namespace GasStation.GraphicEngine
{
    public abstract class ColorSquare : Square, IAnimatedComponent
    {
        Color _baseColor;
        Image _baseImage;
        public Color BaseColor 
        {
            get
            {
                return _baseColor;
            }
            set
            {
                SetDesign(value);
                _baseColor = value;
            }
        }
        public Image BaseImage 
        { 
            get
            {
                return _baseImage;
            }

            set
            {
                SetDesign(value);
                _baseImage = value;
            }

        }
        public ColorSquare(int id, Point location, Size size, ViewComponent viewComponent) : base(id, location, size)
        {
            _pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            BaseColor = viewComponent.Color;
            BaseImage = viewComponent.Image;
            SetDesign(viewComponent.Color);
            SetDesign(viewComponent.Image);
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

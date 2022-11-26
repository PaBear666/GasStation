using GasStation.GraphicEngine.Common;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GasStation.GraphicEngine
{
    public abstract class ColorSquare : Square
    {
        Color _baseBackgroundColor;
        Image _baseBackgroundImage;
        Image _baseFontImage;

        public Color BaseBackgroundColor
        {
            get
            {
                return _baseBackgroundColor;
            }

            set
            {
                _baseBackgroundColor = value;
                PictureBox.BackColor = value;
            }
        }

        public Image BaseBackgroundImage
        {
            get
            {
                return _baseBackgroundImage;
            }

            set
            {
                var clone = (Image)value.Clone();
                _baseBackgroundImage = clone;
                PictureBox.BackgroundImage = clone;
            }
        }

        public Image BaseFrontImage
        {
            get
            {
                return _baseFontImage;
            }

            set
            {
                var clone = (Image)value.Clone();
                _baseFontImage = clone;
                PictureBox.Image = clone;
            }
        }

        public ColorSquare(int id, Point location, Size size) : base(id, location, size)
        {
            BaseBackgroundColor = Control.DefaultBackColor;
        }

        public ColorSquare()
        {

        }

        public virtual void ResetDesign()
        {
            PictureBox.BackColor = BaseBackgroundColor;
            PictureBox.Image = BaseFrontImage;
            PictureBox.BackgroundImage = BaseBackgroundImage;
        }

        public void SetBackgroundColor(Color color)
        {
            PictureBox.BackColor = color;
        }

        public void SetBackgroundImage(Image image)
        {
            PictureBox.BackgroundImage = (Image)image.Clone();
        }

        public void SetFrontImage(Image image)
        {
            PictureBox.Image = (Image)image.Clone();
        }
    }
}

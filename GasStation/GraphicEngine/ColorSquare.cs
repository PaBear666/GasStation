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
                _pictureBox.BackColor = value;
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
                _baseBackgroundImage = value;
                _pictureBox.BackgroundImage = value;
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
                _baseFontImage = value;
                _pictureBox.Image = value;
            }
        }

        public ColorSquare(int id, Point location, Size size) : base(id, location, size)
        {
            BaseBackgroundColor = Control.DefaultBackColor;
        }

        public virtual void ResetDesign()
        {
            _pictureBox.BackColor = BaseBackgroundColor;
            _pictureBox.Image = BaseFrontImage;
            _pictureBox.BackgroundImage = BaseBackgroundImage;
        }

        public void SetBackgroundColor(Color color)
        {
            _pictureBox.BackColor = color;
        }

        public void SetBackgroundImage(Image image)
        {
            _pictureBox.BackgroundImage = image;
        }

        public void SetFrontImage(Image image)
        {
            _pictureBox.Image = image;
        }
    }
}

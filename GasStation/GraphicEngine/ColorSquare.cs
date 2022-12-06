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
                if (value != null)
                {
                    value = (Image)value.Clone();
                }

                _baseBackgroundImage = value;
                PictureBox.BackgroundImage = value;
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
                if(value != null)
                {
                    value = (Image)value.Clone();
                }
                
                _baseFontImage = value;
                PictureBox.Image = value;
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
            if (image != null)
            {
                image = (Image)image.Clone();
            }

            PictureBox.BackgroundImage = image;
        }

        public void SetFrontImage(Image image)
        {
            if (image != null)
            {
                image = (Image)image.Clone();
            }

            PictureBox.Image = image;   
        }
    }
}

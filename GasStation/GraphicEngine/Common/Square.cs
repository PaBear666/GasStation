using System.Drawing;
using System.Windows.Forms;

namespace GasStation.GraphicEngine.Common
{
    public class Square
    {
        readonly PictureBox _pictureBox;

        public Control Control
        {
            get => _pictureBox;
        }

        public Image Image
        {
            get => _pictureBox.Image;
            set => _pictureBox.Image = value;
        }

        public Square(Point location, Size size)
        {
            _pictureBox = new PictureBox()
            {
                Size = size,
                Location = location
            };
        }

       
    }
}

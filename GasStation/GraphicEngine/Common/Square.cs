using System.Drawing;
using System.Windows.Forms;

namespace GasStation.GraphicEngine.Common
{
    public class Square
    {
        protected readonly PictureBox _pictureBox;

        public Control Control
        {
            get => _pictureBox;
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

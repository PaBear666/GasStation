using System.Drawing;
using System.Windows.Forms;

namespace GasStation.GraphicEngine.Common
{
    public class Square
    {
        protected readonly PictureBox _pictureBox;

        public int Id { get; set; }

        public Control Control
        {
            get => _pictureBox;
        }

        public Square(int id, Point location, Size size)
        {
            Id = id;
            _pictureBox = new PictureBox()
            {
                Size = size,
                Location = location
            };
        }
    }
}

using System.Drawing;
using System.Windows.Forms;

namespace GasStation.GraphicEngine.Common
{
    public class Square
    {
        protected readonly PictureBox _pictureBox;
        protected readonly Label _label;
        public int Id { get; set; }

        public Control Control
        {
            get => _pictureBox;
        }

        public Square(int id, Point location, Size size)
        {
            Id = id;
            _pictureBox = new PictureBox
            {
                Size = size,
                Location = location,
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            _label = new Label()
            {
                Text = id.ToString(),
                
            };
            _pictureBox.Controls.Add(_label);
        }

        public Square(PictureBox pictureBox)
        {
            _pictureBox = pictureBox;
            _pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }   
    }
}

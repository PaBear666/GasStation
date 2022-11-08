using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GasStation.GraphicEngine.Common
{
    public class Square : IDisposable
    {
        protected PictureBox _pictureBox;
        protected Label _label;

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
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackgroundImageLayout = ImageLayout.Stretch
                
            };

            #if DEBUG

            _label = new Label()
            {
                Text = id.ToString(),
                BackColor = Color.Transparent,
                ForeColor = Color.White
            };
            _pictureBox.Controls.Add(_label);
            #endif
        }

        public Square()
        {

        }

        public Square(PictureBox pictureBox)
        {
            _pictureBox = pictureBox;
            _pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public virtual void Dispose()
        {
            _pictureBox.Dispose();
            _label.Dispose();
        }
    }
}

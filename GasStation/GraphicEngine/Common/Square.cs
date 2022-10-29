using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GasStation.GraphicEngine.Common
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Square
    {
        protected readonly PictureBox _pictureBox;
        protected readonly Label _label;

        [JsonProperty]
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

            #if DEBUG

            _label = new Label()
            {
                Text = id.ToString(),

            };
            _pictureBox.Controls.Add(_label);
            #endif
        }

        public Square(PictureBox pictureBox)
        {
            _pictureBox = pictureBox;
            _pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }   
    }
}

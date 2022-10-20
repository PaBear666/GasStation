using System;
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
            _pictureBox = new PictureBox()
            {
                Size = size,
                Location = location
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
        }   
    }


    abstract public class SquareDragDrop<T> : Square
        where T : class
    {
        public SquareDragDrop(int id, Point location, Size size) : base(id, location, size)
        {
        }

        public SquareDragDrop(PictureBox pictureBox) : base(pictureBox)
        {

        }

        abstract public T GetDragDropComponent();

        abstract public void FinishDragDrop();
    }
}

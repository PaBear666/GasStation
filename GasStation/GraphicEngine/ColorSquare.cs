using GasStation.GraphicEngine.Common;
using GasStation.GraphicEngine.Common.Abstract;
using System.Drawing;
using System.Windows.Forms;

namespace GasStation.GraphicEngine
{
    public abstract class ColorSquare <T> : SquareDragDrop<T>, IAnimatedComponent
        where T : class
    {
        ViewComponent _baseViewComponent;

        public ViewComponent BaseViewComponent 
        {
            get
            {
                return _baseViewComponent;
            }

            set
            {
                _baseViewComponent = value;
            }
        }

        public ColorSquare(int id, Point location, Size size, ViewComponent viewComponent) : base(id, location, size)
        {
            _pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            BaseViewComponent = viewComponent;
            SetDesign(viewComponent);
        }

        public virtual void ResetDesign()
        {
            _pictureBox.BackColor = _baseViewComponent.Color;
            _pictureBox.Image = _baseViewComponent.Image;
        }

        public virtual void SetDesign(ViewComponent view)
        {
            _pictureBox.BackColor = view.Color;
            _pictureBox.Image = view.Image;
        }
    }
}

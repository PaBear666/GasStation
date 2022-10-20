using GasStation.GraphicEngine.Common;
using GasStation.GraphicEngine.Common.Abstract;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GasStation.GraphicEngine
{
    public abstract class ColorSquare : Square, IAnimatedComponent
    {
        ViewComponent _baseViewComponent;

        protected ViewComponent BaseViewComponent 
        {
            get
            {
                return _baseViewComponent;
            }

            set
            {
                _baseViewComponent = value;
                SetDesign(value);
            }
        }

        public ColorSquare(int id, Point location, Size size, ViewComponent viewComponent) : base(id, location, size)
        {
            BaseViewComponent = viewComponent;
            SetDesign(viewComponent);
        }

        public virtual void ResetDesign()
        {
            _pictureBox.BackColor = BaseViewComponent.Color;
            _pictureBox.Image = BaseViewComponent.Image;
        }

        public virtual void SetDesign(ViewComponent view)
        {
            _pictureBox.BackColor = view.Color;
            _pictureBox.Image = view.Image;
        }
    }
}

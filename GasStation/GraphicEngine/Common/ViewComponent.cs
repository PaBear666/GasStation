using System.Drawing;
using System.Linq.Expressions;

namespace GasStation.GraphicEngine.Common
{
    public class ViewComponent
    {
        Image _image;
        object _keyObject = new object();

        public Image Image 
        {
            get
            {
                lock (_keyObject)
                {
                    return _image;
                }
            }
            private set
            {
                _image = value;
            }
        }
        public Color Color { get; private set; }

        public ViewComponent(Color color, Image image = null)
        {
            Image = image;
            Color = color;
        }
    }
}

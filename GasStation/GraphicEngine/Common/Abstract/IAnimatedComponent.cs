using System.Drawing;

namespace GasStation.GraphicEngine.Common.Abstract
{
    interface IAnimatedComponent
    {
        Color BaseColor { get; set; }
        Image BaseImage { get; set; }

        void SetDesign(Color backColor);
        void SetDesign(Image image);
        void ReturnBaseDesign();
    }
}

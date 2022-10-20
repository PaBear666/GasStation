using System.Drawing;

namespace GasStation.GraphicEngine.Common.Abstract
{
    interface IAnimatedComponent
    {
        void SetDesign(ViewComponent viewComponent);
        void ResetDesign();
    }
}

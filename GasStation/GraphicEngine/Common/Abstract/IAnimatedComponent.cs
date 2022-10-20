using System.Drawing;

namespace GasStation.GraphicEngine.Common.Abstract
{
    interface IAnimatedComponent
    {
        ViewComponent BaseViewComponent { get; set; }

        void SetDesign(ViewComponent viewComponent);
        void ResetDesign();
    }
}

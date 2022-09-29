using GasStation.GraphicEngine.Common;
using GasStation.GraphicEngine.Common.Abstract;
using System.Drawing;

namespace GasStation.LifeEngine
{
    public class Surface : LifeComponent
    {
        public SurfaceType Type { get; set; }
        public Surface(SurfaceType type, ViewComponent viewComponent) : base(viewComponent)
        {
            Type = type;
        }
    }
}
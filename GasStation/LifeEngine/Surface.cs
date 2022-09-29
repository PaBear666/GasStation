using GasStation.GraphicEngine.Common;
using GasStation.GraphicEngine.Common.Abstract;
using System.Drawing;

namespace GasStation.LifeEngine
{
    public class Surface 
    {
        public SurfaceType Type { get; set; }
        public ViewComponent ViewComponent { get; set; }

        public Surface(SurfaceType type, ViewComponent viewComponent)
        {
            Type = type;
            ViewComponent = viewComponent;
        }
    }
}
using GasStation.GraphicEngine.Common;

namespace GasStation.LifeEngine
{
    public abstract class LifeComponent
    {
        public ViewComponent ViewComponent { get; set; }
        public LifeComponent(ViewComponent viewComponent)
        {
            ViewComponent = viewComponent;
        }
    }
}

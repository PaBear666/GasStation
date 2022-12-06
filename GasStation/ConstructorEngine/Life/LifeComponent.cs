using GasStation.GraphicEngine.Common;

namespace GasStation.ConstructorEngine
{
    public abstract class LifeComponent
    {
        public ViewComponent ViewComponent { get; set; }
        public LifeComponent(ViewComponent viewComponent)
        {
            ViewComponent = viewComponent;
        }

        public LifeComponent()
        {

        }
    }
}

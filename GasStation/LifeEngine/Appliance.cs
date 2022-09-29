using GasStation.GraphicEngine.Common;

namespace GasStation.LifeEngine
{
    public class Appliance : LifeComponent
    {
        public ApplianceType Type { get; set; }

        public Appliance(ApplianceType type, ViewComponent component) : base(component)
        {
            Type = type;
        }
    }
}
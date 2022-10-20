using GasStation.GraphicEngine.Common;
using System;

namespace GasStation.LifeEngine
{
    public class Appliance : LifeComponent
    {
        public ApplianceType Type { get; set; }
        public Side Side { get; set; }

        public Appliance(ApplianceType type, Side side, ViewComponent component) : base(component)
        {
            Type = type;
            Side = side;
        }
    }
}
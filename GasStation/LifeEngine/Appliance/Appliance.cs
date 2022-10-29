using GasStation.GraphicEngine.Common;
using Newtonsoft.Json;
using System;

namespace GasStation.LifeEngine
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Appliance : LifeComponent
    {
        [JsonProperty]
        public ApplianceType Type { get; set; }

        [JsonProperty]
        public Side Side { get; set; }

        public Appliance()
        {
            
        }

        public Appliance(ApplianceType type, Side side, ViewComponent component) : base(component)
        {
            Type = type;
            Side = side;
        }
    }
}
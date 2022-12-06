using GasStation.GraphicEngine.Common;
using Newtonsoft.Json;
using System;

namespace GasStation.ConstructorEngine
{
    [JsonObject(MemberSerialization.OptIn)]
    public class LifeAppliance : LifeComponent
    {
        [JsonProperty]
        public Appliance Appliance { get; set; }

        public LifeAppliance()
        {
            
        }

        public LifeAppliance(Appliance appliance, ViewComponent component) : base(component)
        {
            Appliance = appliance;
        }
    }
}
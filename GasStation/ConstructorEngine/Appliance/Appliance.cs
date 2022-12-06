using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.ConstructorEngine
{
    public struct Appliance
    {
        public ApplianceType Type { get; set; }

        public Side Side { get; set; }

        public Appliance(ApplianceType type, Side side)
        {
            Type = type;
            Side = side;
        }
    }
}

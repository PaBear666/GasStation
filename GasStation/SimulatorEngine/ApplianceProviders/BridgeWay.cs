using GasStation.ConstructorEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.SimulatorEngine.ApplianceProviders
{
    public struct BridgeWay
    {
        public SurfaceType To { get; set; }
        public Side Side { get; set; }
        public SurfaceType From { get; set; }

        public BridgeWay(SurfaceType from, SurfaceType to, Side side)
        {
            To = to;
            Side = side;
            From = from;
        }
    }
}

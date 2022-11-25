using GasStation.ConstructorEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.SimulatorEngine.ApplianceSimulators
{
    public class GasStationSimulator : ApplianceSimulator
    {
        public GasStationSimulator(LifeSquare applianceSquare, SimulatorSquare usedSquare) : base(applianceSquare, usedSquare)
        {
        }

        public override void UseSquare()
        {

        }
    }
}

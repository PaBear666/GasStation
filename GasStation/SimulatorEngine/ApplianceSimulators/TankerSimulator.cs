using GasStation.ConstructorEngine;
using GasStation.SimulatorEngine.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.SimulatorEngine.ApplianceSimulators
{
    public class TankerSimulator : ApplianceSimulator<GaslineTankerCar>
    {
        public TankerSimulator(SimulatorSquare applianceSquare, SimulatorSquare usedSquare) : base(applianceSquare, usedSquare)
        {
        }

        public override void UseSquare()
        {

        }
    }
}

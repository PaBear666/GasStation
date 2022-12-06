using GasStation.ConstructorEngine;
using GasStation.SimulatorEngine.Cars;
using System;

namespace GasStation.SimulatorEngine.ApplianceSimulators
{
    public class ShopSimulator : ApplianceSimulator<CollectorCar>
    {
        public ShopSimulator(SimulatorSquare applianceSquare, SimulatorSquare usedSquare) : base(applianceSquare, usedSquare)
        {
        }

        public override void UseSquare()
        {
            throw new NotImplementedException();
        }
    }
}

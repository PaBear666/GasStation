using GasStation.ConstructorEngine;
using GasStation.SimulatorEngine.ApplianceSimulators;
using GasStation.SimulatorEngine.Cars;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.SimulatorEngine.ApplianceProviders
{
    public class ShopProvider : ApplianceProvider<ShopSimulator, CollectorCar>
    {
        public ShopProvider() : base(ApplianceType.Shop)
        {
        }

        public override bool IsCorrect(out string message)
        {
            var baseCorrect = base.IsCorrect(out string errorusedMessage);
            message = errorusedMessage;
            return baseCorrect;
        }
    }
}

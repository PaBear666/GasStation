using GasStation.ConstructorEngine;
using GasStation.SimulatorEngine.ApplianceSimulators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.SimulatorEngine.ApplianceProviders
{
    public class ShopProvider : ApplianceProvider<ShopSimulator>
    {
        public ShopProvider() : base(ApplianceType.Shop)
        {
        }

        public override bool IsCorrect(out string message)
        {
            throw new NotImplementedException();
        }
    }
}

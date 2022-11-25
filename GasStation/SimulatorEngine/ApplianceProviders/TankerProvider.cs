using GasStation.ConstructorEngine;
using GasStation.SimulatorEngine.ApplianceSimulators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.SimulatorEngine.ApplianceProviders
{
    public class TankerProvider : ApplianceProvider<TankerSimulator>
    {
        public TankerProvider() : base(ApplianceType.Tanker)
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

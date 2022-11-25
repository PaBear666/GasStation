using GasStation.SimulatorEngine.ApplianceSimulators;
using GasStation.ConstructorEngine;

namespace GasStation.SimulatorEngine.ApplianceProviders
{
    public class GasStationProvider : ApplianceProvider<GasStationSimulator>
    {
        
        public GasStationProvider() : base(ApplianceType.GasStation)
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

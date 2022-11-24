using GasStation.ConstructorEngine;
using GasStation.SimulatorEngine.ApplianceSimulators;
using System.Collections.Generic;
using System.Linq;

namespace GasStation.SimulatorEngine.ApplianceProviders
{
    abstract public class ApplianceProvider<A>
        where A : ApplianceSimulator
    {
        public ApplianceType ApplianceType { get; protected set; }

        public IEnumerable<A> Appliances { get; protected set; }

        public abstract void AddApliances();

        public abstract bool IsCorrect(out string message);

        public virtual bool TryUseAppliance(int usedSquareByApplianceId)
        {
            var usedSquare = Appliances.FirstOrDefault(a => a.UsedSquare.Id == usedSquareByApplianceId);

            if(usedSquare != null)
            {
                usedSquare.UseSquare();
                return true;
            }

            return false;
        }
    }
}

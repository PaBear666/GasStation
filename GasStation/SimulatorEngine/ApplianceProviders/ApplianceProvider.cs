using GasStation.ConstructorEngine;
using System.Collections.Generic;
using System.Linq;

namespace GasStation.SimulatorEngine.ApplianceProviders
{
    abstract public class ApplianceProvider<A>
        where A : ApplianceSimulator
    {
        public ApplianceType ApplianceType { get; }

        public IEnumerable<A> Appliances { get; set; }

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

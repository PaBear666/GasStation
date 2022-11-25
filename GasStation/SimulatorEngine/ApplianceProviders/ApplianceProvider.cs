using GasStation.ConstructorEngine;
using GasStation.SimulatorEngine.ApplianceSimulators;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GasStation.SimulatorEngine.ApplianceProviders
{
    abstract public class ApplianceProvider<A>
        where A : ApplianceSimulator
    {
        public ApplianceType ApplianceType { get; }

        public ICollection<A> Appliances { get; protected set; }

        public ApplianceProvider(ApplianceType applianceType)
        {
            ApplianceType = applianceType;
            Appliances = new List<A>();
        }

        public virtual bool IsCorrect(out string message)
        {
            var stringBulder = new StringBuilder();
            bool absCorrected = true;
            foreach (var appliance in Appliances)
            {
                bool currentCorrected = true;
                currentCorrected = appliance.UsedSquare != null;
                absCorrected = currentCorrected;

                if (!currentCorrected)
                {
                    stringBulder.AppendLine($"Объект {appliance.ApplianceSquare.Id} не имеет клетки взаимодействия");
                }
            }

            if(Appliances.Count == 0)
            {
                stringBulder.AppendLine($"Должене быть хотя бы один объект типа {ApplianceType}");
                absCorrected = false;
            }

            message = stringBulder.ToString();
            return absCorrected;
        }

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

using GasStation.ConstructorEngine;
using GasStation.SimulatorEngine.ApplianceSimulators;
using GasStation.SimulatorEngine.Cars;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GasStation.SimulatorEngine.ApplianceProviders
{
    abstract public class ApplianceProvider<A,C>
        where A : ApplianceSimulator<C>
        where C : SimulatorCar
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

        public void UseAppliances()
        {
            foreach (var appliance in Appliances)
            {
                appliance.UseSquare();
            }
        }

    }
}

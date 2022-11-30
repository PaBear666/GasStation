using GasStation.ConstructorEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.SimulatorEngine.ApplianceSimulators
{
    public abstract class ApplianceSimulator
    {
        public LifeSquare ApplianceSquare { get; set; }
        public SimulatorSquare UsedSquare { get; set; }
        public bool IsFree
        {
            get
            {
                return UsedSquare.Car == null || UsedSquare.Car.State == Cars.CarState.UseAppliance;
            }
        }
        public ApplianceSimulator(LifeSquare applianceSquare, SimulatorSquare usedSquare)
        {
            ApplianceSquare = applianceSquare;
            UsedSquare = usedSquare;
        }

        public abstract void UseSquare();
    }
}

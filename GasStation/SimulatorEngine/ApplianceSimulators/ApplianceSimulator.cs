using GasStation.ConstructorEngine;
using GasStation.SimulatorEngine.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.SimulatorEngine.ApplianceSimulators
{
    public abstract class ApplianceSimulator<C>
        where C : SimulatorCar
    {

        public SimulatorSquare ApplianceSquare { get; set; }
        public SimulatorSquare UsedSquare { get; set; }
        public Queue<C> Cars { get; set; }
        public bool IsFree
        {
            get
            {
                return MaxCar > Cars.Count;
            }
        }

        public int MaxCar { get; set; }
        public ApplianceSimulator(SimulatorSquare applianceSquare, SimulatorSquare usedSquare)
        {
            Cars = new Queue<C>();
            ApplianceSquare = applianceSquare;
            UsedSquare = usedSquare;
            MaxCar = 1;
        }

        public abstract void UseSquare();
    }
}

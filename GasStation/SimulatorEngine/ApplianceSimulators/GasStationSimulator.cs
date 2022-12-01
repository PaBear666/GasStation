using GasStation.ConstructorEngine;
using GasStation.SimulatorEngine.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.SimulatorEngine.ApplianceSimulators
{
    public class GasStationSimulator : ApplianceSimulator<CommonCar>
    {
        const int _fuelPower = 10;
        CommonCar _currentCar;
        public GasStationSimulator(SimulatorSquare applianceSquare, SimulatorSquare usedSquare) : base(applianceSquare, usedSquare)
        {
        }

        public override void UseSquare()
        {
            if(_currentCar == null && Cars.Count > 0)
            {
                var car = Cars.Peek();
                if(car != null && car.State == CarState.UseAppliance)
                {
                    
                    _currentCar = Cars.Dequeue();
                }
                return;
            }

            if(_currentCar != null && _currentCar.State == CarState.UseAppliance)
            {
                _currentCar.Fuel += _fuelPower;
            }
            else
            {
                _currentCar = null;
            }
        }
    }
}

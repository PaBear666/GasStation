using GasStation.ConstructorEngine;
using GasStation.DB;
using GasStation.SimulatorEngine.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.SimulatorEngine.ApplianceSimulators
{
    
    public class TankerSimulator : ApplianceSimulator<GaslineTankerCar>
    {
        public Fuel Fuel { get; set; }
        public int Id { get; set; }
        public int[] MaxVolume { get; set; }
        GaslineTankerCar _currentCar;
        public int[] volume;
        
        public TankerSimulator(SimulatorSquare applianceSquare, SimulatorSquare usedSquare,int count) : base(applianceSquare, usedSquare)
        {
            Id = count;
        }

        public override void UseSquare()
        {
            if (_currentCar == null && Cars.Count > 0)
            {
                var car = Cars.Peek();
                if (car != null && car.State == CarState.UseAppliance)
                {

                    _currentCar = Cars.Dequeue();
                }
                return;
            }

            if (_currentCar != null && _currentCar.State == CarState.UseAppliance)
            {
                TankerConnector.Volume[TankerConnector.FindFuel(_currentCar.FuelV.Type)] += FuelRate.FuelSpeed;
                _currentCar.FuelGiven = TankerConnector.MaxVolume[TankerConnector.FindFuel(_currentCar.FuelV.Type)] - TankerConnector.Volume[TankerConnector.FindFuel(_currentCar.FuelV.Type)];
            }
            else
            {
                
                _currentCar = null;
            }
         }

        
    }
}

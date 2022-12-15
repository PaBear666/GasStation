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
        private int _fuelPower;
        CommonCar _currentCar;
        
        public GasStationSimulator(SimulatorSquare applianceSquare, SimulatorSquare usedSquare) : base(applianceSquare, usedSquare)
        {
            //MaxCar = 1;
            _fuelPower = FuelRate.FuelPower;
        }
        
        public override void UseSquare()
        {
            if(!Check(TankerConnector.CanSpawnTankerCar)||!TankerConnector.CanSpawnCollectorCar)
            {
                for(int i = 0; i < Cars.Count; i++)
                {
                    Cars.Where(car => car is CommonCar).ToList().ForEach(car => { 
                        (car as CommonCar).NeedDispawn = true;
                        (car as CommonCar).ToSquare = TankerConnector.DisspawnSquare;
                     });
                }
            }
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
                TankerConnector.Volume[TankerConnector.FindFuel(_currentCar.FuelV.Type)] -= _fuelPower;
                
            }
            else
            {
                _currentCar = null;
            }
        }
        private bool Check(bool[] a)
        {
            for(int i= 0; i < a.Length; i++)
            {
                if(!a[i]) return false;
            }
            return true;   
        }
    }
}

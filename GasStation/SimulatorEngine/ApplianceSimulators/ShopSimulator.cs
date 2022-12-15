using GasStation.ConstructorEngine;
using GasStation.SimulatorEngine.Cars;
using System;

namespace GasStation.SimulatorEngine.ApplianceSimulators
{
    public class ShopSimulator : ApplianceSimulator<CollectorCar>
    {
        public double MoneyVolume { get; set; }
        CollectorCar _currentCar;
        public ShopSimulator(SimulatorSquare applianceSquare, SimulatorSquare usedSquare) : base(applianceSquare, usedSquare)
        {
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
                TankerConnector.CurrentMoney-= FuelRate.MoneyRate;
                _currentCar.MoneyTaken = TankerConnector.CurrentMoney-(TankerConnector.MaxMoney*TankerConnector.MoneyPrecent);
            }
            else
            {

                _currentCar = null;
            }
        }
    }
}

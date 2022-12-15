using GasStation.ConstructorEngine;
using GasStation.DB;
using GasStation.GraphicEngine.Common;
using GasStation.SimulatorEngine.ApplianceSimulators;

namespace GasStation.SimulatorEngine.Cars
{
    public class CommonCar : SimulatorCar
    {
        private int _fuel;
        public Fuel FuelV { get; set; }
        
        public int Fuel
        {
            get
            {
                return _fuel;
            }
            set
            {
                _fuel += value;
                TankerConnector.CurrentMoney += FuelV.Cost;
                TankerConnector.Volume[TankerConnector.FindFuel(FuelV.Type)] -= value;
                if (TankerConnector.Volume[TankerConnector.FindFuel(FuelV.Type)] <= 0)
                {
                    TankerConnector.Volume[TankerConnector.FindFuel(FuelV.Type)] = 0;
                    NeedDispawn = true;
                    TankerConnector.CanFill[TankerConnector.FindFuel(FuelV.Type)] = true;
                }
                if (TankerConnector.CurrentMoney >= TankerConnector.MaxMoney*TankerConnector.MoneyPrecent)
                {
                    NeedDispawn = true;
                    TankerConnector.MoneyReplacing = true;
                }
                if (_fuel >= MaxFuel)
                {
                    _fuel = MaxFuel;
                    NeedDispawn = true;
                }

                if(_fuel < 0)
                {
                    _fuel = 0;
                }
            }
        }
        public int MaxFuel { get; set; }
        public override CarState State
        {
            get
            {
                
                if ( Fuel < MaxFuel && CurrentSquare.Id == ToSquare.Id)
                {
                    return CarState.UseAppliance;
                }
                return CarState.ToAppliance;
            }
        }
        public CommonCar(ViewComponent viewComponent, SimulatorSquare to, SimulatorSquare current, CommnonCarViewType type) : 
            base(current,
                to,
                SurfaceType.GasStation,
                ApplianceType.GasStation,
                CarType.CommonCar,
                viewComponent)
        {
            base.CarViewType = type;
            MaxFuel = 3000;
        }
    }
}

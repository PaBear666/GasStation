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
        public int GasStationIndex { get; set; }
        bool firstuse = true;
        public int Fuel
        {
            get
            {
                return _fuel;
            }
            set
            {
                //TankerConnector.CurrentMoney -= FuelV.Cost * _fuel;
                _fuel += value;
                int i = TankerConnector.FindFuel(FuelV.Type);
                TankerConnector.CurrentMoney += FuelV.Cost * value;
                TankerConnector.Volume[i] -= value;
                if (TankerConnector.Volume[i] <= 0)
                {
                    TankerConnector.Volume[i] = 0;
                    NeedDispawn = true;
                    TankerConnector.CanFill[i] = true;
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
                ViewCounterProvider.LastCheck[GasStationIndex] = _fuel * FuelV.Cost;
                ViewCounterProvider.LastFill[GasStationIndex] = _fuel;
                ViewCounterProvider.Fdg();
            }
        }
        public int MaxFuel { get; set; }
        public override CarState State
        {
            get
            {
                
                if ( Fuel < MaxFuel && CurrentSquare.Id == ToSquare.Id)
                {
                    if(firstuse)
                    {
                        ViewCounterProvider.LastCheck[GasStationIndex] = 0;
                        ViewCounterProvider.LastFill[GasStationIndex] = 0;
                        firstuse = false;
                    }
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

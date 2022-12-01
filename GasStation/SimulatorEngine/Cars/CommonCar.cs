using GasStation.ConstructorEngine;
using GasStation.GraphicEngine.Common;

namespace GasStation.SimulatorEngine.Cars
{
    public class CommonCar : SimulatorCar
    {
        private int _fuel;
        public int Fuel
        {
            get
            {
                return _fuel;
            }
            set
            {
                _fuel = value;
                if(MaxFuel >= _fuel)
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
                if (Fuel < MaxFuel && CurrentSquare.Id == ToSquare.Id)
                {
                    return CarState.UseAppliance;
                }

                return CarState.ToAppliance;
            }
        }
        public CommonCar(ViewComponent viewComponent, SimulatorSquare to, SimulatorSquare current) : 
            base(current,
                to,
                SurfaceType.GasStation,
                ApplianceType.GasStation,
                CarType.CommonCar,
                viewComponent)
        {
            MaxFuel = 100;
        }
    }
}

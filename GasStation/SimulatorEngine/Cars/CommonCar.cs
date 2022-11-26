using GasStation.ConstructorEngine;
using GasStation.GraphicEngine.Common;

namespace GasStation.SimulatorEngine.Cars
{
    public class CommonCar : SimulatorCar
    {
        public CommonCar(ViewComponent viewComponent, SimulatorSquare to, SimulatorSquare current) : 
            base(current,
                to,
                SurfaceType.GasStation,
                ApplianceType.GasStation,
                CarType.CommonCar,
                viewComponent)
        {

        }
    }
}

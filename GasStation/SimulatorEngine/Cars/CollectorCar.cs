using GasStation.ConstructorEngine;
using GasStation.GraphicEngine.Common;

namespace GasStation.SimulatorEngine.Cars
{
    public class CollectorCar : SimulatorCar
    {
        public CollectorCar(ViewComponent viewComponent, SimulatorSquare to, SimulatorSquare current) :
            base(current,
                to,
                SurfaceType.GasStation,
                ApplianceType.Shop,
                CarType.Сollector,
                viewComponent)
        {

        }

        public override CarState State => throw new System.NotImplementedException();
    }
}

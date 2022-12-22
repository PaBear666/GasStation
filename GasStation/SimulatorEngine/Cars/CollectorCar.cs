using GasStation.ConstructorEngine;
using GasStation.GraphicEngine.Common;
using GasStation.SimulatorEngine.ApplianceSimulators;

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
        public double MoneyTaken { set { if (value <= 0) { TankerConnector.MoneyReplacing = false; NeedDispawn = true; TankerConnector.CanSpawnCollectorCar = true; TankerConnector.CurrentMoney = 0; } ViewCounterProvider.Fdg(); } }

        public override CarState State
        {
            get
            {
                if (TankerConnector.CurrentMoney > 0 && CurrentSquare.Id == ToSquare.Id)
                {
                    return CarState.UseAppliance;
                }
                return CarState.ToAppliance;
            }
        }
    }
}

using GasStation.ConstructorEngine;
using GasStation.DB;
using GasStation.GraphicEngine.Common;
using GasStation.SimulatorEngine.ApplianceSimulators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.SimulatorEngine.Cars
{
    public class GaslineTankerCar : SimulatorCar
    {
        public Fuel FuelV { get; set; }
        /*
        public int Fuel
        {
          
        }*/
        public const int FuelRate = 100;
        public int MaxFuel { get; set; }

        public int FuelGiven { set { if (value <= 0) { TankerConnector.CanFill[TankerConnector.FindFuel(FuelV.Type)] = false; NeedDispawn = true; TankerConnector.CanSpawnTankerCar[TankerConnector.FindFuel(FuelV.Type)] = true; } } }
        public GaslineTankerCar(ViewComponent viewComponent, SimulatorSquare to, SimulatorSquare current) :
            base(current,
                to,
                SurfaceType.Service,
                ApplianceType.Tanker,
                CarType.GasolineTanker,
                viewComponent)
        {
        }

        public override CarState State
        {
            get
            {
                int i = TankerConnector.FindFuel(FuelV.Type);
                if (TankerConnector.Volume[i] < TankerConnector.MaxVolume[i] && CurrentSquare.Id == ToSquare.Id)
                {
                    return CarState.UseAppliance;
                }
                return CarState.ToAppliance;
            }
        }
    }
}

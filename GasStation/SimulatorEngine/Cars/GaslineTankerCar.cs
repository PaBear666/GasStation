using GasStation.ConstructorEngine;
using GasStation.GraphicEngine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.SimulatorEngine.Cars
{
    public class GaslineTankerCar : SimulatorCar
    {
        public GaslineTankerCar(ViewComponent viewComponent, SimulatorSquare to, SimulatorSquare current) :
            base(current,
                to,
                SurfaceType.Service,
                ApplianceType.Tanker,
                CarType.GasolineTanker,
                viewComponent)
        {
        }

        public override CarState State => throw new NotImplementedException();
    }
}

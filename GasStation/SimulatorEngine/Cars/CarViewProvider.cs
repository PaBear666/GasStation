using GasStation.GraphicEngine.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.SimulatorEngine.Cars
{
    public class CarViewProvider
    {
        public IDictionary<CarType, ViewComponent> Car { get; private set; }

        public CarViewProvider()
        {
            Car = new Dictionary<CarType, ViewComponent>()
            {
                { CarType.CommonCar, new ViewComponent(Color.Red, Resource.car) },
                { CarType.Сollector, new ViewComponent(Color.Green, Resource.car) },
                { CarType.GasolineTanker, new ViewComponent(Color.Blue, Resource.car) },
            };
        }
    }
}

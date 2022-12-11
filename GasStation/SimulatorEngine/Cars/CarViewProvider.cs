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

        public ViewComponent GetView(CarType type) 
        {
            var random = new Random().Next(1,4);

            switch (type)
            {
                case CarType.CommonCar:
                    switch (random)
                    {
                        case 1:
                            return new ViewComponent(Color.Red, Resource.car);
                        case 2:
                            return new ViewComponent(Color.Red, Resource.GreenCarZombi);
                        case 3:
                            return new ViewComponent(Color.Red, Resource.StreetRace);
                        default:
                            return null;
                    }

                    break;
                case CarType.Сollector:
                    return null;
                    
                case CarType.GasolineTanker:
                    return null;
                default:
                    return null;
  
            }
        }
    }
}

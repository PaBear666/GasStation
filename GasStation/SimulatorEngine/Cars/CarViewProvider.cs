using GasStation.ConstructorEngine;
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
        public CommnonCarViewType CarVType { get; private set; }
    
        
        public ViewComponent GetView(CarType type) 
        {
            var random = new Random().Next(1,4);

            switch (type)
            {
                case CarType.CommonCar:
                    switch (random)
                    {
                        case 1:
                            CarVType = CommnonCarViewType.Red;
                            return new ViewComponent(Color.Red, Resource.car);
                        case 2:
                            CarVType = CommnonCarViewType.Green;
                            return new ViewComponent(Color.Red, Resource.GreenCarZombi);
                        case 3:
                            CarVType = CommnonCarViewType.Blue;
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
        
        public static Image GetSide(CarType type, Side side,CommnonCarViewType viewType)
        {
            var random = new Random().Next(1, 4);

            switch (type)
            {
                case CarType.CommonCar:
                    switch (viewType)
                    {
                        case CommnonCarViewType.Red:
                            {
                                switch(side)
                                {
                                    case Side.Left:
                                        return Resource.carLeft;
                                    case Side.Right:
                                        return Resource.car;
                                    case Side.Top:
                                        return Resource.carTop;
                                    case Side.Bottom:
                                        return Resource.carBot;
                                    default:
                                        return Resource.carBot;
                                }
                                
                            }
                        case CommnonCarViewType.Green:
                            {
                                switch (side)
                                {
                                    case Side.Left:
                                        return Resource.GreenCarZombiLeft;
                                    case Side.Right:
                                        return Resource.GreenCarZombi;
                                    case Side.Top:
                                        return Resource.GreenCarZombiTop;
                                    case Side.Bottom:
                                        return Resource.GreenCarZombiBot;
                                    default:
                                        return Resource.GreenCarZombi;
                                }

                            }
                        case CommnonCarViewType.Blue:
                            {
                                switch (side)
                                {
                                    case Side.Left:
                                        return Resource.StreetRaceLeft;
                                    case Side.Right:
                                        return Resource.StreetRace;
                                    case Side.Top:
                                        return Resource.StreetRaceTop;
                                    case Side.Bottom:
                                        return Resource.StreetRaceBot;
                                    default:
                                        return Resource.StreetRace;
                                }

                            }
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

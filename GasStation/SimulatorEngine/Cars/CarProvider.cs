using GasStation.ConstructorEngine;
using GasStation.GraphicEngine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GasStation.SimulatorEngine.Cars
{
    public class CarProvider
    {
        SimulatorSquare _spawnSquare;
        SimulatorSquare _disspawnSqaure;
        readonly CarViewProvider _carViewProvider;
        readonly SimulatorSquare[] _squares;
        readonly int _width;
        readonly int _height;
        readonly Wave _wave;

        public SimulatorSquare SpawnSquare
        {
             get
             {
                 return _spawnSquare;
             }
             set
             {
                 if (value.Surface.Type != SurfaceType.Road)
                 {
                     throw new Exception("Точку появления можно ставить только на дороге");
                 }

                _spawnSquare = value;
             }
        }
        public SimulatorSquare DisspawnSquare
        {
            get
            {
                return _disspawnSqaure;
            }
            set
            {
                if (value.Surface.Type != SurfaceType.Road)
                {
                    throw new Exception("Точку исчезноваения можно ставить только на дороге");
                }

                _disspawnSqaure = value;
            }
        }

        public ICollection<SimulatorCar> _cars;

        public CarProvider(
            SimulatorSquare spawnSquare,
            SimulatorSquare disspawnSquare,
            SimulatorSquare[] squares,
            int width,
            int height,
            Wave wave)
        {
            SpawnSquare = spawnSquare;
            DisspawnSquare = disspawnSquare;
            _carViewProvider = new CarViewProvider();
            _cars = new List<SimulatorCar>();
            _squares = squares;
            _width = width;
            _height = height;
            _wave = wave;
        }

        public bool SpawnCar(SimulatorCar car)
        {
            var isSpawn = _spawnSquare.Car == null;
            if (!isSpawn)
            {
                return false;
            }

            _cars.Add(car);
            _spawnSquare.Car = car;
            return true;
        }

        public void SimulateCar()
        {
            var disspawnCars = _cars.Where(c => c.CurrentSquare.Id == DisspawnSquare.Id && c.NeedDispawn).ToList();
            foreach (var car in disspawnCars)
            {
                DeleteCar(car);
            }

            foreach (var car in _cars)
            {
                if(car.ToSquare != null)
                {
                    if (_wave.TryGetSide(car.AvailableSurfaceType, car.CurrentSquare.Id, car.ToSquare.Id, out var side))
                    {
                        if (side.HasValue)
                        {
                            MoveCar(car, side.Value);
                        }
                    }
                }

                if(car.ToSquare == null || car.NeedDispawn)
                {
                    car.ToSquare = DisspawnSquare;
                    car.NeedDispawn = true;
                }
            }
        }

        private void DeleteCar(SimulatorCar simulatorCar)
        {
            var currentSquare = simulatorCar.CurrentSquare;
            currentSquare.Car = null;

            _cars.Remove(simulatorCar);
        }

        private void MoveCar(SimulatorCar simulatorCar,Side side)
        {
            var currentSquare = simulatorCar.CurrentSquare;
            var sideSquare = SquareHelper.GetArroundSquare(_squares, currentSquare, _height, _width, side);

            if(sideSquare != null && sideSquare.Car == null)
            {
                sideSquare.Car = simulatorCar;
                simulatorCar.CurrentSquare = sideSquare;
                currentSquare.Car = null;
            }  
        }
    }
}

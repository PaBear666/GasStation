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
        CarViewProvider _carViewProvider;
        SimulatorSquare[] _squares;
        int _width;
        int _height;
        Form _form;
        Wave _wave;

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

        public ICollection<SimulatorCar> _сars;

        public CarProvider(
            SimulatorSquare spawnSquare,
            SimulatorSquare disspawnSquare,
            SimulatorSquare[] squares,
            int width,
            int height,
            Form form,
            Wave wave)
        {
            SpawnSquare = spawnSquare;
            DisspawnSquare = disspawnSquare;
            _carViewProvider = new CarViewProvider();
            _сars = new List<SimulatorCar>();
            _squares = squares;
            _width = width;
            _height = height;
            _form = form;
            _wave = wave;
        }

        public void SpawnCar(CarType carType)
        {
            if(_spawnSquare.Car != null)
            {
                return;
            }

            switch (carType)
            {
                case CarType.CommonCar:
                    var car = new CommonCar(_carViewProvider.Car[carType], DisspawnSquare, SpawnSquare);
                    _сars.Add(car);
                    _form.BeginInvoke(new Action(() => _spawnSquare.Car = car));
                    break;
                case CarType.Сollector:
                    break;
                case CarType.GasolineTanker:
                    break;
                default:
                    break;
            }
        }

        public void SimulateCar()
        {
            var disspawnCars = _сars.Where(c => c.CurrentSquare.Id == DisspawnSquare.Id).ToList();
            foreach (var car in disspawnCars)
            {
                DeleteCar(car);
            }

            var movingCars = _сars.Where(c => c.State == CarState.Stand).ToList();
            foreach (var car in movingCars)
            {
                MoveCar(car, Side.Left);
            }
        }

        private void DeleteCar(SimulatorCar simulatorCar)
        {
            _form.BeginInvoke(new Action(() =>
            {
                var currentSquare = simulatorCar.CurrentSquare;
                currentSquare.Car = null;
            }));

            _сars.Remove(simulatorCar);
        }

        private void MoveCar(SimulatorCar simulatorCar,Side side)
        {
            var currentSquare = simulatorCar.CurrentSquare;
            var sideSquare = SquareHelper.GetArroundSquare(_squares, currentSquare, _height, _width, side);

            if(sideSquare != null && sideSquare.Car == null)
            {
                _form.BeginInvoke(new Action(() =>
                {
                    sideSquare.Car = simulatorCar;
                    simulatorCar.CurrentSquare = sideSquare;
                    currentSquare.Car = null;
                }));
            }  
        }
    }
}

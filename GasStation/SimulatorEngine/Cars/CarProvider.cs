using GasStation.ConstructorEngine;
using System;

namespace GasStation.SimulatorEngine.Cars
{
    public class CarProvider
    {
        SimulatorSquare _spawnSquare;
        SimulatorSquare _disspawnSqaure;
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

        public CarProvider(SimulatorSquare spawnSquare, SimulatorSquare disspawnSquare)
        {
            SpawnSquare = spawnSquare;
            DisspawnSquare = disspawnSquare;
        }
    }
}

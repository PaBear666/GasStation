using GasStation.ConstructorEngine;
using GasStation.SimulatorEngine.Cars;
using System.Drawing;
using System;

namespace GasStation.SimulatorEngine
{
    public class SimulatorSquare : LifeSquare
    {
        private SimulatorCar _car;
        public SimulatorCar Car
        {
            get
            {
                return _car;
            }
            set
            {

            }
        }
        public SimulatorSquare(int id, Point location, Size size, Surface surface) : base(id, location, size, surface)
        {

        }
    }
}

using GasStation.ConstructorEngine;
using GasStation.SimulatorEngine.Cars;
using System.Drawing;

namespace GasStation.SimulatorEngine
{
    public class SimulatorSquare : LifeSquare
    {
        public bool WillBeAvailable { get; set; }
        public SimulatorCar Car { get; set; }
        public SimulatorSquare(int id, Point location, Size size, Surface surface) : base(id, location, size, surface)
        {
            WillBeAvailable = true;
        }
    }
}

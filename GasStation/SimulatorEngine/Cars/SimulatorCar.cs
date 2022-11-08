using GasStation.ConstructorEngine;
using GasStation.GraphicEngine.Common;
using System.Drawing;

namespace GasStation.SimulatorEngine.Cars
{
    public abstract class SimulatorCar : ViewComponent
    {
        SurfaceType AvailableSurfaceType { get; set; }

        Appliance Appliance { get; set; }

        public SimulatorCar(Color color, Image image = null) : base(color, image)
        {

        }
    }
}

using System.Drawing;

namespace GasStation.LifeEngine
{
    public class Simulation
    {
        public SimulatorType Type { get; set; }

        public Surface Surface { get; set; }

        public Image Image { get; set; }

        public Color Color { get; set; }

        public Simulation(Surface surface, SimulatorType type, Color color, Image image = null)
        {
            Type = type;
            Surface = surface;
            Color = color;
            Image = image;
        }
    }
}
using GasStation.GraphicEngine;
using GasStation.LifeEngine;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GasStation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var a = new ConstructorArea(panel1, 50, 10);

            a.SurfaceSetuper.ChooseSurface(SurfaceType.GasStation);
        }
    }
}

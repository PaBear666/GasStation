using GasStation.GraphicEngine.Common;
using GasStation.LifeEngine;
using System.Windows.Forms;

namespace GasStation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var surfaceProvider = new SurfaceProvider();
            var a = new ConstructorArea(panel1, surfaceProvider, 50, 10);
            var b = new AppliancePictureBox(new Appliance(ApplianceType.Tanker, Side.Right, surfaceProvider.GetSurface(SurfaceType.GasStation).ViewComponent), pictureBox3);
        }
    }
}

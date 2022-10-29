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

            var editorProvider = new EditorProvider();
            var a = new ConstructorArea(panel1, editorProvider, 50, 10);
            var b = new AppliancePictureBox(editorProvider.Appliance[ApplianceType.GasStation], pictureBox3);
            b.EndDragDrop += a.EndDrop;
            b.StartDrop += a.ShowAvailableZone;
        }
    }
}

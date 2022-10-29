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
            var constructor = new ConstructorArea(panel1,Side.Bottom, editorProvider, 50, 10);
            var gasStationAppliance = new AppliancePictureBox(editorProvider.Appliance[ApplianceType.GasStation], pictureBox3);
            var tankerAppliance = new AppliancePictureBox(editorProvider.Appliance[ApplianceType.Tanker], pictureBox2);
            var shopAppliance = new AppliancePictureBox(editorProvider.Appliance[ApplianceType.Shop], pictureBox1);

            gasStationAppliance.EndDragDrop += constructor.EndDrop;
            gasStationAppliance.StartDrop += constructor.ShowAvailableZone;

            tankerAppliance.EndDragDrop += constructor.EndDrop;
            tankerAppliance.StartDrop += constructor.ShowAvailableZone;

            shopAppliance.EndDragDrop += constructor.EndDrop;
            shopAppliance.StartDrop += constructor.ShowAvailableZone;

        }

        
    }
}

using GasStation.GraphicEngine.Common;
using GasStation.LifeEngine;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace GasStation
{
    public partial class Form1 : Form
    {
        ConstructorArea _constructor;
        public Form1()
        {
            InitializeComponent();

            var editorProvider = new EditorProvider();
             _constructor = new ConstructorArea(panel1,Side.Left, editorProvider, 50, 10);
            var gasStationAppliance = new AppliancePictureBox(editorProvider.Appliance[ApplianceType.GasStation], pictureBox3);
            var tankerAppliance = new AppliancePictureBox(editorProvider.Appliance[ApplianceType.Tanker], pictureBox2);
            var shopAppliance = new AppliancePictureBox(editorProvider.Appliance[ApplianceType.Shop], pictureBox1);

            gasStationAppliance.EndDragDrop += _constructor.EndDrop;
            gasStationAppliance.StartDrop += _constructor.ShowAvailableZone;

            tankerAppliance.EndDragDrop += _constructor.EndDrop;
            tankerAppliance.StartDrop += _constructor.ShowAvailableZone;

            shopAppliance.EndDragDrop += _constructor.EndDrop;
            shopAppliance.StartDrop += _constructor.ShowAvailableZone;



        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            var a = _constructor.GetTransfer("as");
            var serializer = JsonConvert.SerializeObject(a);

        }
    }
}

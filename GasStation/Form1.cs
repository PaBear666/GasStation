using GasStation.GraphicEngine.Common;
using GasStation.LifeEngine;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace GasStation
{
    public partial class Form1 : Form
    {
        readonly ConstructorArea _constructor;
        public Form1()
        {
            InitializeComponent();

            var editorProvider = new EditorProvider();
             _constructor = new ConstructorArea(panel1,Side.Bottom, editorProvider, 50, 10);

            AddAppliancePictureBox(editorProvider, ApplianceType.OutBridge, pictureBox5);
            AddAppliancePictureBox(editorProvider, ApplianceType.InBridge, pictureBox4);
            AddAppliancePictureBox(editorProvider, ApplianceType.GasStation, pictureBox3);
            AddAppliancePictureBox(editorProvider, ApplianceType.Tanker, pictureBox2);
            AddAppliancePictureBox(editorProvider, ApplianceType.Shop, pictureBox1);
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            var a = _constructor.GetTransfer("as");
            var serializer = JsonConvert.SerializeObject(a);
        }

        public AppliancePictureBox AddAppliancePictureBox(EditorProvider editorProvider, ApplianceType applianceType, PictureBox pictureBox)
        {
            var appliance = new AppliancePictureBox(editorProvider.Appliance[applianceType], pictureBox);
            appliance.EndDragDrop += _constructor.EndDrop;
            appliance.StartDrop += _constructor.ShowAvailableZone;
            return appliance;
        }
    }
}

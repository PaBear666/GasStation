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
            _constructor = new ConstructorArea(panel1, Side.Bottom, editorProvider, 10, 7);
            AddAppliancePictureBox(editorProvider, new Appliance(ApplianceType.Bridge, Side.Top), pictureBox4);
            AddAppliancePictureBox(editorProvider, new Appliance(ApplianceType.GasStation, Side.Bottom), pictureBox3);
            AddAppliancePictureBox(editorProvider, new Appliance(ApplianceType.Tanker, Side.Bottom), pictureBox2);
            AddAppliancePictureBox(editorProvider, new Appliance(ApplianceType.Shop, Side.Bottom), pictureBox1);
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            var a = _constructor.GetTransfer("as");
            var serializer = JsonConvert.SerializeObject(a);
        }

        public AppliancePictureBox AddAppliancePictureBox(EditorProvider editorProvider, Appliance appliance, PictureBox pictureBox)
        {
            var appliancePicture = new AppliancePictureBox(editorProvider, appliance, pictureBox);
            appliancePicture.EndDragDrop += _constructor.EndDrop;
            appliancePicture.StartDrop += _constructor.ShowAvailableZone;
            return appliancePicture;
        }
    }
}

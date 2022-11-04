using GasStation.GraphicEngine.Common;
using GasStation.LifeEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GasStation
{
    public partial class Form1 : Form
    {
        ConstructorArea _constructor;
        ICollection<AppliancePictureBox> _appliancePicturesBoxes;
        EditorProvider _editorProvider;

        public Form1()
        {
            InitializeComponent();
            _editorProvider = new EditorProvider();


    }

        private void SaveTopology(object sender, System.EventArgs e)
        {
            var a = _constructor.GetTransfer("as");
            var serializer = JsonConvert.SerializeObject(a);
        }

        private void InitAppliacnePictureBox()
        {
            _appliancePicturesBoxes = new List<AppliancePictureBox>()
            {
                AddAppliancePictureBox(_editorProvider, new Appliance(ApplianceType.Bridge, Side.Top), pictureBox4),
                AddAppliancePictureBox(_editorProvider, new Appliance(ApplianceType.GasStation, Side.Bottom), pictureBox3),
                AddAppliancePictureBox(_editorProvider, new Appliance(ApplianceType.Tanker, Side.Bottom), pictureBox2),
                AddAppliancePictureBox(_editorProvider, new Appliance(ApplianceType.Shop, Side.Bottom), pictureBox1),
            };
        }

        public AppliancePictureBox AddAppliancePictureBox(EditorProvider editorProvider, Appliance appliance, PictureBox pictureBox)
        {
            var appliancePicture = new AppliancePictureBox(editorProvider, appliance, pictureBox);
            appliancePicture.EndDragDrop += _constructor.EndDrop;
            appliancePicture.StartDrop += _constructor.ShowAvailableZone;
            return appliancePicture;
        }

        private void UploadTopology(object sender, System.EventArgs e)
        {

        }

        private void NewConstructor(object sender, System.EventArgs e)
        {
            _constructor = new ConstructorArea(panel1, Side.Bottom, _editorProvider, 10, 7);
            InitAppliacnePictureBox();
        }
    }
}

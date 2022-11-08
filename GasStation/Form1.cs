using GasStation.GraphicEngine.Common;
using GasStation.LifeEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GasStation
{
    public partial class Form1 : Form
    {
        private ConstructorArea _constructor;
        private List<AppliancePictureBox> _appliancePicturesBoxes;
        private EditorProvider _editorProvider;
        private string _lastSaved;

        public Form1()
        {
            InitializeComponent();
            _editorProvider = new EditorProvider();


    }

        private void SaveTopology(object sender, System.EventArgs e)
        {
            var a = _constructor.GetTransfer("as");
            _lastSaved = JsonConvert.SerializeObject(a);
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
            return new AppliancePictureBox(editorProvider, appliance, pictureBox);
        }

        public void RemoveAppliacneEventcPictureBox()
        {
            foreach (var appliancePictureBox in _appliancePicturesBoxes)
            {
                appliancePictureBox.EndDragDrop -= _constructor.EndDrop;
                appliancePictureBox.StartDrop -= _constructor.ShowAvailableZone;
                appliancePictureBox.Dispose();
            }
        }

        public void SetAppliacneEventcPictureBox()
        {
            foreach (var appliancePictureBox in _appliancePicturesBoxes)
            {
                appliancePictureBox.EndDragDrop += _constructor.EndDrop;
                appliancePictureBox.StartDrop += _constructor.ShowAvailableZone;
            }
        }

        private void UploadTopology(object sender, System.EventArgs e)
        {
            if(_lastSaved != null)
            {
                
                if(_constructor != null)
                {
                    RemoveAppliacneEventcPictureBox();
                    _constructor.Dispose();
                }

                InitAppliacnePictureBox();
              
                var topology = JsonConvert.DeserializeObject<TopologyTransfer>(_lastSaved);
                _constructor = new ConstructorArea(panel1, topology, _editorProvider);
                SetAppliacneEventcPictureBox();
            }
        }

        private void NewConstructor(object sender, System.EventArgs e)
        {
            if(_constructor != null)
            {
                RemoveAppliacneEventcPictureBox();
                _constructor.Dispose();
            }

            InitAppliacnePictureBox();

            _constructor = new ConstructorArea(panel1, Side.Bottom, _editorProvider, 10, 7);
            SetAppliacneEventcPictureBox();
        }
    }
}

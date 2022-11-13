using GasStation.GraphicEngine.Common;
using GasStation.LifeEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GasStation
{
    public partial class Form1 : Form
    {
        private ConstructorArea _constructor;
        private ICollection<AppliancePictureBox> _appliancePicturesBoxes;
        private readonly EditorProvider _editorProvider;
        private string _lastSaved;

        public Form1()
        {
            InitializeComponent();
            _editorProvider = new EditorProvider();
        }

        private void SaveTopology(object sender, System.EventArgs e)
        {
            if (_constructor != null)
            {
                var a = _constructor.GetTransfer("as");
                _lastSaved = JsonConvert.SerializeObject(a);
            }
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
            return new AppliancePictureBox(
                editorProvider,
                (a) => _constructor.ApplianceCount[a] < _editorProvider.MaxAplianceOnMap[a],
                appliance,
                pictureBox);
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

        private void ApplianceUpdate(object sender, IDictionary<ApplianceType, int> e)
        {
            label1.Text = (_editorProvider.MaxAplianceOnMap[ApplianceType.Shop] - e[ApplianceType.Shop]).ToString();
            label2.Text = (_editorProvider.MaxAplianceOnMap[ApplianceType.Tanker] - e[ApplianceType.Tanker]).ToString();
            label3.Text = (_editorProvider.MaxAplianceOnMap[ApplianceType.GasStation] - e[ApplianceType.GasStation]).ToString();
            label4.Text = (_editorProvider.MaxAplianceOnMap[ApplianceType.Bridge] - e[ApplianceType.Bridge]).ToString();
        }

        private void UploadTopology(object sender, System.EventArgs e)
        {
            if (_lastSaved != null)
            {

                if (_constructor != null)
                {
                    RemoveAppliacneEventcPictureBox();
                    _constructor.Dispose();
                }

                InitAppliacnePictureBox();

                var topology = JsonConvert.DeserializeObject<TopologyTransfer>(_lastSaved);
                _constructor = new ConstructorArea(panel1, topology, ApplianceUpdate, _editorProvider);
                SetAppliacneEventcPictureBox();
            }
        }

        private void NewConstructor(object sender, System.EventArgs e)
        {
            SizesForm sizesForm = new SizesForm();
            sizesForm.ShowDialog();
            if (sizesForm.Ok)
            {
                if (_constructor != null)
                {
                    RemoveAppliacneEventcPictureBox();
                    _constructor.Dispose();
                }

                InitAppliacnePictureBox();

                _constructor = new ConstructorArea(panel1, Side.Bottom, _editorProvider, ApplianceUpdate, sizesForm.Width, sizesForm.Height);
                SetAppliacneEventcPictureBox();
            }
        }
    }
}

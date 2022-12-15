using GasStation.ConstructorEngine;
using GasStation.DB;
using GasStation.GraphicEngine.Common;
using GasStation.SimulatorEngine;
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
            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";

            ViewTapologyDb.ViewTopologys(listBox1);
            if (listBox1.Items.Count > 0)
                listBox1.SelectedIndex = 0;
        }

        private void SaveTopology(object sender, System.EventArgs e)
        {
            if (_constructor != null)
            {
                var a = _constructor.GetTransfer();
                _lastSaved = JsonConvert.SerializeObject(a);
                ViewTapologyDb.SaveTopology(listBox1, _lastSaved);
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

                var topology = JsonConvert.DeserializeObject<TopologyTransfer>(ViewTapologyDb.LoadTopology(listBox1.SelectedIndex));
                _editorProvider.side = topology.RowSide;
                _constructor = new ConstructorArea(panel1, topology, ApplianceUpdate, _editorProvider);
                SetAppliacneEventcPictureBox();
            }
        }

        private void NewConstructor(object sender, System.EventArgs e)
        {
            if (_constructor != null)
            {
                RemoveAppliacneEventcPictureBox();
                _constructor.Dispose();
            }

            InitAppliacnePictureBox();
            TopologyCreationForm form = new TopologyCreationForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                _editorProvider.side = form.side;
                _constructor = new ConstructorArea(panel1, form.side, _editorProvider, ApplianceUpdate, form.W, form.H);
                var a = _constructor.GetTransfer();
                _lastSaved = JsonConvert.SerializeObject(a);
                ViewTapologyDb.AddTopologytoNew(listBox1, _lastSaved);
                ViewTapologyDb.ViewTopologys(listBox1);
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
                SetAppliacneEventcPictureBox();
            }
        }

        private void Simulation(object sender, System.EventArgs e)
        {
            if (_lastSaved != null)
            {
                var topology = JsonConvert.DeserializeObject<TopologyTransfer>(_lastSaved);
                var simulatorWindow = new Simulator(topology);
                simulatorWindow.ShowDialog();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                if (_constructor != null)
                {
                    RemoveAppliacneEventcPictureBox();
                    _constructor.Dispose();
                }

                InitAppliacnePictureBox();
                _lastSaved = ViewTapologyDb.LoadTopology(listBox1.SelectedIndex);
                var topology = JsonConvert.DeserializeObject<TopologyTransfer>(_lastSaved);
                _editorProvider.side = topology.RowSide;
                _constructor = new ConstructorArea(panel1, topology, ApplianceUpdate, _editorProvider);
                SetAppliacneEventcPictureBox();
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                ViewTapologyDb.RemoveTopology(listBox1);
                RemoveAppliacneEventcPictureBox();
                _constructor.Dispose();
                _constructor = null;
            }
        }
    }
}
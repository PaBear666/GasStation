using GasStation.ConstructorEngine;
using GasStation.DB;
using GasStation.SimulatorEngine;
using GasStation.SimulatorEngine.ApplianceSimulators;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GasStation
{
    public partial class Simulator : Form
    {
        private EditorProvider _editorProvider;
        private SimulatorArea area;
        private CancellationTokenSource _cancellation;
    
        private int _takerCount;
        public Simulator(TopologyTransfer topology)
        {
            InitializeComponent();
          button2.Enabled = false;

            _editorProvider = new EditorProvider();
            _editorProvider.side = topology.RowSide;
            _cancellation = new CancellationTokenSource();

            area = new SimulatorArea(panel1, topology, _editorProvider, _cancellation.Token);
           TankerConnector.CarSpawnSide=topology.RowSide;
            var a = area.IsCorrect;
            var b = area.ErrorMessage;

            if (!area.IsCorrect)
            {
                richTextBox1.AppendText("Ошибка!!!");
                richTextBox1.AppendText(Environment.NewLine);
                richTextBox1.AppendText(b);
            }
             
            this.FormClosing += (e, c) => _cancellation.Cancel();
            Width = 1290;
            _takerCount = area.TankerCount;
            if (!area.IsCorrect)
            {
                button1.Enabled = false;
                button3.Enabled = false;
            }
        }
        
        private void Simulate(object sender, EventArgs e)
        {
            button3.Enabled = false;
            ViewCounterProvider.DataGrid = dataGridView2;
            ViewCounterProvider.DataGridTanker = dataGridView1;
            ViewCounterProvider.Label = label3;
            area.Run();
        }

        private void Stop(object sender, EventArgs e)
        {
            area.Stop();
        }

      

        private void button3_Click_1(object sender, EventArgs e)
        {
            ModelConrolForm md = new ModelConrolForm(_takerCount, 1);
            if (md.ShowDialog() == DialogResult.OK)
            {
                button2.Enabled = true;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = trackBar1.Value.ToString();
            area.Acceleration = 5+trackBar1.Value;
        }
    }
}

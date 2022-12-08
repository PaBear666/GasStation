using GasStation.ConstructorEngine;
using GasStation.DB;
using GasStation.SimulatorEngine;
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
        private List<Topology> _topology;
        public Simulator(TopologyTransfer topology)
        {
            InitializeComponent();
          button2.Enabled = false;

            _editorProvider = new EditorProvider();
            _cancellation = new CancellationTokenSource();
            area = new SimulatorArea(panel1, topology, _editorProvider, _cancellation.Token);
            
            var a = area.IsCorrect;
            var b = area.ErrorMessage;

            if (!area.IsCorrect)
            {
                richTextBox1.AppendText("Ошибка!!!");
                richTextBox1.AppendText(Environment.NewLine);
                richTextBox1.AppendText(b);
            }

            this.FormClosing += (e, c) => _cancellation.Cancel();
        }

      

        private void Simulate(object sender, EventArgs e)
        {
            area.Run();
        }
        private void Stop(object sender, EventArgs e)
        {
            area.Stop();
        }

     

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            ModelConrolForm md = new ModelConrolForm(2, 4);
            md.ShowDialog();
            this.Show();
            button2.Enabled = true;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = trackBar1.Value.ToString();
            area.Acceleration = trackBar1.Value;
        }
    }
}

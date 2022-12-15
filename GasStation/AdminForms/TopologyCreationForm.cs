using GasStation.DB;
using GasStation.DB.Controller;
using GasStation.ConstructorEngine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GasStation
{
    public partial class TopologyCreationForm : Form
    {
        public Side side { get; set; }
        public int W { get; set; }
        public int H { get; set; }
        public TopologyCreationForm()
        {
            InitializeComponent();
            down.Checked = true;
            W = trackBar1.Value;
            H = trackBar2.Value;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Wcounterlabel.Text = trackBar1.Value.ToString();
            W=trackBar1.Value;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
           LcounterLabel.Text = trackBar2.Value.ToString();
            H=trackBar2.Value;
        }

        private void left_CheckedChanged(object sender, EventArgs e)
        {
            side = Side.Left;
            trackBar2.Minimum = 4;
        }

        private void right_CheckedChanged(object sender, EventArgs e)
        {
            side= Side.Right;
            trackBar2.Minimum = 4;
        }

        private void up_CheckedChanged(object sender, EventArgs e)
        {
            side = Side.Top;
            trackBar2.Minimum = 3;
        }

        private void down_CheckedChanged(object sender, EventArgs e)
        {
            side = Side.Bottom;
            trackBar2.Minimum = 3;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool f = true;
            Panel panel = new Panel();
            EditorProvider _editorProvider = new EditorProvider();
            try
            {
                ConstructorArea _constructor = new ConstructorArea(panel, side, _editorProvider, null, trackBar1.Value, trackBar2.Value); ;
                var a = _constructor.GetTransfer();
                string _lastSaved = JsonConvert.SerializeObject(a);
                DataBaseContext context = new DataBaseContext();
                List<Topology> t = context.Topologies.ToList();

                foreach (Topology t2 in t)
                {
                    if (t2.Name == textBox1.Text)
                    {
                        f = false;
                        break;
                    }
                }
                if (f)
                {
                    TopologyController.createTopology(textBox1.Text, _lastSaved);
                    MessageBox.Show("Топология успешно добавлена");
                    if(up.Checked||down.Checked)
                    {
                        W += 4;
                        H ++;
                    }
                    else
                    {
                        W ++;
                        H += 4;
                    }
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                    MessageBox.Show("Топлогия с именем:" + textBox1.Text + " уже существует");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

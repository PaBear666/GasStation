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
        public TopologyCreationForm()
        {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Wcounterlabel.Text = trackBar1.Value.ToString();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
           LcounterLabel.Text = trackBar2.Value.ToString();
        }

        private void left_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void right_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void up_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void down_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}

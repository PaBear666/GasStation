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
    public partial class SizesForm : Form
    {
        public bool Ok { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public SizesForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ok = true;
            Width = (int)numericUpDown1.Value;
            Height = (int)numericUpDown2.Value;
            Close();
        }
    }
}

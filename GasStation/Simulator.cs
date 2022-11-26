using GasStation.ConstructorEngine;
using GasStation.SimulatorEngine;
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
    public partial class Simulator : Form
    {
        private EditorProvider _editorProvider;
        private SimulatorArea area;
        public Simulator(TopologyTransfer topology)
        {
            InitializeComponent();
            _editorProvider = new EditorProvider();
            area = new SimulatorArea(panel1, topology, _editorProvider, this);

            var a = area.IsCorrect;
            var b = area.ErrorMessage;

            if (!area.IsCorrect)
            {
                richTextBox1.AppendText("Ошибка!!!");
                richTextBox1.AppendText(Environment.NewLine);
                richTextBox1.AppendText(b);
            }

        }

        private void Simulate(object sender, EventArgs e)
        {
            area.Run();
        }

        private void CreateCar(object sender, EventArgs e)
        {

        }
    }
}

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
        public Simulator(TopologyTransfer topology)
        {
            InitializeComponent();
            _editorProvider = new EditorProvider();
            var simulatorArea = new SimulatorArea(panel1, topology, _editorProvider);

            var a = simulatorArea.IsCorrect;
            var b = simulatorArea.ErrorMessage;

            if (!simulatorArea.IsCorrect)
            {
                richTextBox1.AppendText("Ошибка долбаеб");
                richTextBox1.AppendText(Environment.NewLine);
                richTextBox1.AppendText(b);
            }

        }
    }
}

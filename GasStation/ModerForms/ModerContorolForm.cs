using GasStation.ConstructorEngine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GasStation
{
    public partial class ModerContorolForm : Form
    {
        public ModerContorolForm()
        {
            ModelConrolForm userControl = new ModelConrolForm(7, 2);
            Form1 form1 = new Form1();
            InitializeComponent();
            ViewTapologyDb.ViewTopologys(listBox1);
            userControl = (ModelConrolForm)this.SetupForm(userControl);
            if (listBox1.Items.Count > 0)
                listBox1.SelectedIndex = 0;
            Width = 2200;
        }

        private Form SetupForm(Form form)
        {
            form.TopLevel = false;
            form.Visible = true;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            return form;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                var topology = JsonConvert.DeserializeObject<TopologyTransfer>(ViewTapologyDb.LoadTopology(listBox1.SelectedIndex));
                var simulatorWindow = new Simulator(topology);
                Simulator form1 = new Simulator(topology);
                form1 = (Simulator)this.SetupForm(form1);
                this.tabControl1.TabPages[0].Controls.Clear();
                this.tabControl1.TabPages[0].Controls.Add(form1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();

            }
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start($"{Environment.CurrentDirectory}/../../../Help.chm");
            }
            catch (Exception)
            {
                MessageBox.Show("Файл справки не найден!", "Ошибка!");
            }
        }
    }
}
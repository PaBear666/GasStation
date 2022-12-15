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
    public partial class AdminPanel : Form
    {
        UserControl userControl = new UserControl();
        FuelControlForm ffc = new FuelControlForm();
        Form1 form1 = new Form1();
        TransportControlForm transportControl = new TransportControlForm();

        public AdminPanel()
        {
            UserControl userControl = new UserControl();
            FuelControlForm ffc = new FuelControlForm();
            TransportControlForm transportControl = new TransportControlForm();
            Form1 form1 = new Form1();
            InitializeComponent();
            userControl = (UserControl)this.SetupForm(userControl);
            ffc = (FuelControlForm)this.SetupForm(ffc);
            form1 = (Form1)this.SetupForm(form1);
            transportControl = (TransportControlForm)this.SetupForm(transportControl);

            this.tabControl1.TabPages[0].Controls.Add(userControl);
            this.tabControl1.TabPages[1].Controls.Add(ffc);
            this.tabControl1.TabPages[2].Controls.Add(transportControl);
            this.tabControl1.TabPages[3].Controls.Add(form1);
            Width = 1300;
        }
        private Form SetupForm(Form form)
        {
            form.TopLevel = false;
            form.Visible = true;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            return form;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            userControl.Dispose();
            ffc.Dispose();
            form1.Dispose();
            transportControl.Dispose();
            //GC.Collect(4, GCCollectionMode.Forced);
            //GC.GetTotalMemory(true);
            userControl = new UserControl();
            ffc = new FuelControlForm();
            form1 = new Form1();
            transportControl = new TransportControlForm();

            userControl = (UserControl)this.SetupForm(userControl);
            ffc = (FuelControlForm)this.SetupForm(ffc);
            form1 = (Form1)this.SetupForm(form1);
            transportControl = (TransportControlForm)this.SetupForm(transportControl);
            this.tabControl1.TabPages[0].Controls.Clear();
            this.tabControl1.TabPages[0].Controls.Add(userControl);
            this.tabControl1.TabPages[1].Controls.Clear();
            this.tabControl1.TabPages[1].Controls.Add(ffc);
            this.tabControl1.TabPages[2].Controls.Clear();
            this.tabControl1.TabPages[2].Controls.Add(transportControl);
            this.tabControl1.TabPages[3].Controls.Clear();
            this.tabControl1.TabPages[3].Controls.Add(form1);

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

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
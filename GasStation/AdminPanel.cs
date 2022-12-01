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
    public partial class AdminPanel : Form
    {
        public AdminPanel()
        {
            UserControl userControl = new UserControl();
            FuelControlForm ffc = new FuelControlForm();
            TransportControlForm transportControl = new TransportControlForm();
            InitializeComponent();
            userControl = (UserControl)this.SetupForm(userControl);
            ffc = (FuelControlForm)this.SetupForm(ffc);
            transportControl = (TransportControlForm)this.SetupForm(transportControl);
            this.tabControl1.TabPages[0].Controls.Add(userControl);
            this.tabControl1.TabPages[1].Controls.Add(ffc);
            this.tabControl1.TabPages[2].Controls.Add(transportControl);
        }
        private Form SetupForm(Form form)
        {
            form.TopLevel = false;
            form.Visible = true;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            return form;

        }
    }
}

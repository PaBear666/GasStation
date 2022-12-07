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
    public partial class ModerContorolForm : Form
    {
        public ModerContorolForm()
        {
            ModelConrolForm userControl = new ModelConrolForm(7,2);
           
            InitializeComponent();
            userControl = (ModelConrolForm)this.SetupForm(userControl);
            
            this.tabControl1.TabPages[0].Controls.Add(userControl);
            
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
    }
}

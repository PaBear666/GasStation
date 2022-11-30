using GasStation.DB;
using GasStation.DB.Controller;
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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            PasswordTextBox.UseSystemPasswordChar = true;

        }

        private void LoginButton_Click(object sender, EventArgs e)
        {


            if (UserController.LogUser(LoginTextBox.Text, PasswordTextBox.Text))

            {

                this.Hide();
                UserControl userControl = new UserControl();
                userControl.ShowDialog();
                this.Show();
            }
            else
                MessageBox.Show("Ошибка ввода");
        }

        private void ShowPasswordeСheckBox_CheckedChanged(object sender, EventArgs e)
        {
            PasswordTextBox.UseSystemPasswordChar = !PasswordTextBox.UseSystemPasswordChar;
        }
    }
}

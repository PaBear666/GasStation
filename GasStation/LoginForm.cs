using GasStation.DB;
using GasStation.DB.Controller;
using GasStation.MathLogic;
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
            Random rnd = new Random();
            PasswordTextBox.UseSystemPasswordChar = true;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            switch(UserController.LogUser(LoginTextBox.Text, PasswordTextBox.Text))
            {
                case UserType.Admin:
                    {
                        this.Hide();
                        AdminPanel userControl = new AdminPanel();
                        userControl.ShowDialog();
                        this.Show();
                        break;
                    }
                case UserType.Moderator:
                    {
                        this.Hide();
                        ModerContorolForm userControl = new ModerContorolForm();
                        userControl.ShowDialog();
                        this.Show();
                        break;
                    }
                case UserType.NoN:
                    {
                        MessageBox.Show("Данные для входа введены неверно");
                        break;
                    }
            }


        }

        private void ShowPasswordeСheckBox_CheckedChanged(object sender, EventArgs e)
        {
            PasswordTextBox.UseSystemPasswordChar = !PasswordTextBox.UseSystemPasswordChar;
        }
    }
}

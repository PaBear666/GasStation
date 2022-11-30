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
    public partial class UserControl : Form
    {
        User MainUser;
        List<User> users;
        DataBaseContext context = new DataBaseContext();
        const string PasswordConst = "********";
        int SelectedRaw=-1;
        bool canDelete = true;
        UserType userType;
        public UserControl()
        {
            InitializeComponent();
            
            FillDataGride();
           
        }

        private void EditUser_Click(object sender, EventArgs e)
        {
            
            for (int i = 0; i < users.Count; i++)
            {
                string name = dataGridView1.Rows[i].Cells[0].Value.ToString();
                UserType type = (UserType)dataGridView1.Rows[i].Cells[1].Value;
                string password = dataGridView1.Rows[i].Cells[2].Value.ToString();
                if (!password.Equals(PasswordConst) || users[i].Name != name || users[i].UserRole != type)
                {
                    
                    User newUser = new User();
                    bool flag = true;
                    bool shouldCheckName = users[i].Name != name;
                    if (shouldCheckName)
                    {
                        foreach (User u in users)
                        {
                            if (u.Name.ToLower().Equals(name.ToLower()))
                            { flag = false; break; }
                        }
                    }
                    if(!password.Equals(PasswordConst) && !(password.Length>4))
                    {
                        MessageBox.Show("Пароль у пользователя с логином: " + name + " должен быть больше 4 символов");
                        break;
                    }


                    if (flag)
                    {
                        newUser.Name = name;
                        newUser.UserRole = type;
                        if (password != PasswordConst)
                            newUser.Password = password;
                        UserController.EditUser(users[i], newUser);

                    }
                    else
                    {
                        MessageBox.Show("Пользователь с логином: "+ name +" уже есть");
                        
                        break;
                    }
                }
            }
            if (dataGridView1.Rows[users.Count].Cells[0].Value!=null)
                AddUser();
            FillDataGride();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        

        private void FillDataGride()
        {
            canDelete = false;
            context = new DataBaseContext();
            users = context.Users.ToList();
            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add(users.Count+1);

            for (int i = 0; i < users.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value=users[i].Name;
                dataGridView1.Rows[i].Cells[1].Value=users[i].UserRole;
                dataGridView1.Rows[i].Cells[2].Value = PasswordConst;
                
            }
            canDelete = true;
        }

    

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedRaw = e.RowIndex;

            if (e.RowIndex >= 0)
            {

                if (e.ColumnIndex == 1)
                {
                    

                    if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = UserType.Moderator;
                    else
                    {
                        if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == UserType.Moderator.ToString())
                            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = UserType.Admin;
                        else
                            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = UserType.Moderator;
                    }
                }


                /*
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                }
                */
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddUser();
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(SelectedRaw>-1)
            {
                UserController.Remove(users[SelectedRaw]);
                FillDataGride();
            }
            else
            {
                MessageBox.Show("Элемент из списка для удаления не выбран");
            }
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (canDelete)
            {
                UserController.Remove(users[e.RowIndex]);
                FillDataGride();
            }
        }

        private void AddUser()
        {
            bool flag = true;
            int i = dataGridView1.Rows.Count - 1;
            if (dataGridView1.Rows[i].Cells[0].Value != null && dataGridView1.Rows[i].Cells[1].Value != null && dataGridView1.Rows[i].Cells[2] != null)
            {
                foreach (User a in users)
                {
                    if (a.Name.ToLower() == dataGridView1.Rows[i].Cells[0].Value.ToString().ToLower())
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    if (dataGridView1.Rows[i].Cells[2].Value.ToString().Length > 4)
                    {
                        string error = UserController.createUser(dataGridView1.Rows[i].Cells[0].Value.ToString(), (UserType)dataGridView1.Rows[i].Cells[1].Value, dataGridView1.Rows[i].Cells[2].Value.ToString());
                        if (error != null)
                            MessageBox.Show(error);
                        else
                        {
                            FillDataGride();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Длина пароля должна быть больше 4 символов");
                    }
                }
                else
                    MessageBox.Show("Пользователь с таким логином уже есть");
            }
            else
            {
                MessageBox.Show("Не все поля заполнены для создания пользователя");
            }
        }
    }
}

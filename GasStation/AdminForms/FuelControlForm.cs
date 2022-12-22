using GasStation.DB;
using GasStation.DB.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GasStation
{
    public partial class FuelControlForm : Form
    {
        DataBaseContext context = new DataBaseContext();
        List<Fuel> fuels;
        bool canDelete = true;

        public FuelControlForm()
        {
            InitializeComponent();
            FillDataGride();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FuelControlForm_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            dataGridView2.EditingControl.KeyPress -= EditingControl_KeyPress;
            dataGridView2.EditingControl.KeyPress += EditingControl_KeyPress;
        }

        private void EditingControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)&& dataGridView2.CurrentCell.ColumnIndex==1)
            {
                Control editingControl = (Control)sender;
                string s = "";
                if(e.KeyChar=='.')e.KeyChar = ',';
                if (dataGridView2.CurrentCell.EditedFormattedValue!=null)
                     s = dataGridView2.CurrentCell.EditedFormattedValue.ToString();
                if (!((char.IsDigit(e.KeyChar) && find_dot(s)) || (e.KeyChar == ',' && !s.Contains(",") && s.Length > 0)))
                    e.Handled = true;
            }
        }
       
 
        
        bool find_dot(string str)
        {
            string [] s  = str.Split(',');
            try
            {
                if (s[1].Length > 1)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                return true;
            }
        }
        private void FillDataGride()
        {
            canDelete = false;
            context = new DataBaseContext();
            fuels = context.Fuels.ToList();
            dataGridView2.Rows.Clear();
            dataGridView2.Rows.Add(fuels.Count + 1);

            for (int i = 0; i < fuels.Count; i++)
            {
                dataGridView2.Rows[i].Cells[0].Value = fuels[i].Type;
                dataGridView2.Rows[i].Cells[1].Value = Math.Round(fuels[i].Cost,2).ToString();
                

            }
            canDelete = true;
        }

        private void addFuelButton_Click(object sender, EventArgs e)
        {
            AddFuel();
        }




        private void AddFuel()
        {
            bool flag = true;
            int i = dataGridView2.Rows.Count - 1;
            if (dataGridView2.Rows[i].Cells[0].Value != null && dataGridView2.Rows[i].Cells[1].Value != null )
            {
                foreach (Fuel a in fuels)
                {
                    if (a.Type.ToLower() == dataGridView2.Rows[i].Cells[0].Value.ToString().ToLower())
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                   
                        string error = FuelController.createFuel(dataGridView2.Rows[i].Cells[0].Value.ToString(), Double.Parse(dataGridView2.Rows[i].Cells[1].Value.ToString()));
                        if (error != null)
                            MessageBox.Show(error);
                        else
                        {
                            FillDataGride();
                        }
                   
                }
                else
                    MessageBox.Show("Топливо с таким типом уже есть");
            }
            else
            {
                MessageBox.Show("Не все поля заполнены для создания топлива");
            }
        }

        private void EditFuelButton_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < fuels.Count; i++)
            {
                string type = dataGridView2.Rows[i].Cells[0].Value.ToString();
                Double cost = Double.Parse(dataGridView2.Rows[i].Cells[1].Value.ToString());
               
                if (fuels[i].Type != type || Math.Round( fuels[i].Cost,2) != Math.Round(cost,2))
                {

                    Fuel newFuel = new Fuel();
                    bool flag = true;
                    bool shouldCheckType = fuels[i].Type != type;
                    if (shouldCheckType)
                    {
                        foreach (Fuel f in fuels)
                        {
                            if (f.Type.ToLower().Equals(type.ToLower()))
                            { flag = false; break; }
                        }
                    }

                    if (flag)
                    {
                        newFuel.Type = type;
                        newFuel.Cost = cost;
                        
                        FuelController.EditFuel(fuels[i], newFuel);

                    }
                    else
                    {
                        MessageBox.Show("Топливо с типом: " + type + " уже есть");

                        break;
                    }
                }
            }
            if (dataGridView2.Rows[fuels.Count].Cells[0].Value != null)
                AddFuel();
            FillDataGride();
        }

        private void dataGridView2_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {
                if (canDelete)
                {
                    if (FuelController.Remove(fuels[e.RowIndex]) != null)
                        MessageBox.Show("Невозможно удалить так как, есть транспорт с таким типом топлива. " +
                            "Прежде удалить транспорт содержащий данный вид топлива");
                    FillDataGride();
                }
            }
    
            catch (Exception ex)
            {
                    dataGridView2.Rows.Add( 1);
            }
        }
    }
}

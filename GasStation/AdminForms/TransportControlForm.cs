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
    public partial class TransportControlForm : Form
    {
        DataBaseContext context = new DataBaseContext();
        List<Transport> transports;
        List<Fuel> fuels;
        bool canDelete = true;
        int SelectedRaw = -1;
        public TransportControlForm()
        {
            InitializeComponent();
            fuels = context.Fuels.ToList();
            FillDataGride();

        }
        private void FillDataGride()
        {
            fuels = context.Fuels.ToList();
            canDelete = false;
            context = new DataBaseContext();
            transports = context.Transports.ToList();
            dataGridView2.Rows.Clear();
            dataGridView2.Rows.Add(transports.Count + 1);

            for (int i = 0; i < transports.Count; i++)
            {
                dataGridView2.Rows[i].Cells[0].Value = transports[i].Name;
                try
                {
                    dataGridView2.Rows[i].Cells[1].Value = transports[i].Fuel.Type;
                }
                catch { dataGridView2.Rows[i].Cells[1].Value = null; }
                dataGridView2.Rows[i].Cells[2].Value = transports[i].FuelVolume;
            }
            canDelete = true;
        }




        private void addFuelButton_Click(object sender, EventArgs e)
        {
            AddTransport();
        }

        private void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            dataGridView2.EditingControl.KeyPress -= EditingControl_KeyPress;
            dataGridView2.EditingControl.KeyPress += EditingControl_KeyPress;
        }

        private void EditingControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && dataGridView2.CurrentCell.ColumnIndex == 2)
            {
                if (!char.IsDigit(e.KeyChar))
                    e.Handled = true;
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedRaw = e.RowIndex;

            if (e.RowIndex >= 0)
            {

                if (e.ColumnIndex == 1)
                {


                    if (dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                        dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = fuels[0];
                    else
                    {

                    }
                }
            }
        }



        private void AddTransport()
        {
            bool flag = true;
            int i = dataGridView2.Rows.Count - 1;
            if (dataGridView2.Rows[i].Cells[0].Value != null && dataGridView2.Rows[i].Cells[1].Value != null && dataGridView2.Rows[i].Cells[2].Value != null)
            {
                foreach (Transport a in transports)
                {
                    if (a.Name.ToLower() == dataGridView2.Rows[i].Cells[0].Value.ToString().ToLower())
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    if (Int32.TryParse(dataGridView2.Rows[i].Cells[2].Value.ToString(),out int res))
                    {
                        string error = TransportController.createTransport(dataGridView2.Rows[i].Cells[0].Value.ToString(),Int32.Parse( dataGridView2.Rows[i].Cells[2].Value.ToString()), fuels[findFuelID(dataGridView2.Rows[i].Cells[1].Value.ToString())] );
                        if (error != null)
                            MessageBox.Show(error);
                        else
                        {
                            FillDataGride();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Число некорректно");
                    }
                }
                else
                    MessageBox.Show("Транспорт с такой маркой уже есть");
            }
            else
            {
                MessageBox.Show("Не все поля заполнены для создания транспорта");
            }
        }

        private void dataGridView2_MouseClick(object sender, MouseEventArgs e)
        {
            if(dataGridView2.CurrentCell.ColumnIndex==1)
            {
                int r = dataGridView2.CurrentCell.RowIndex;
                if (dataGridView2.Rows[r].Cells[1].Value == null)
                {
                    dataGridView2.Rows[r].Cells[1].Value= fuels[0].Type;
                }
                else
                {
                    string s = dataGridView2.Rows[r].Cells[1].Value.ToString();
                    int i = findFuelID(s);
                    if(e.Button == MouseButtons.Left)
                    {
                        i--;
                        if(i < 0)
                            dataGridView2.Rows[r].Cells[1].Value = fuels[fuels.Count-1].Type;
                        else
                            dataGridView2.Rows[r].Cells[1].Value = fuels[i].Type;
                    }
                    if (e.Button == MouseButtons.Right)
                    {
                        i++;
                        if (i >= fuels.Count)
                            dataGridView2.Rows[r].Cells[1].Value = fuels[0].Type;
                        else
                            dataGridView2.Rows[r].Cells[1].Value = fuels[i].Type;
                    }
                }
            }
        }

        int findFuelID(string s)
        {
            
            for(int i=0;i<fuels.Count;i++)
            {
                if(s.Equals(fuels[i].Type))
                    return i;
            }
            return 0;
        }

        private void EditTransportButton_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < transports.Count; i++)
            {
                string name = dataGridView2.Rows[i].Cells[0].Value.ToString();
                string fuel = dataGridView2.Rows[i].Cells[1].Value.ToString();
                int fuelVolume = 0;
                if (Int32.TryParse(dataGridView2.Rows[i].Cells[2].Value.ToString(), out fuelVolume))
                {
                    if (transports[i].Name != name || transports[i].Fuel.Type != fuel || transports[i].FuelVolume != fuelVolume)
                    {

                        Transport newTransport = new Transport();
                        bool flag = true;
                        bool shouldCheckName = transports[i].Name != name;
                        if (shouldCheckName)
                        {
                            foreach (Transport u in transports)
                            {
                                if (u.Name.ToLower().Equals(name.ToLower()))
                                { flag = false; break; }
                            }
                        }



                        if (flag)
                        {
                            newTransport.Name = name;
                            newTransport.Fuel = fuels[findFuelID(fuel)];
                            newTransport.FuelVolume = fuelVolume;
                            TransportController.EditTransport(transports[i], newTransport);

                        }
                        else
                        {
                            MessageBox.Show("Транспорт с маркой: " + name + " уже есть");

                            break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Число выходит за диапозоны допустимых значений");
                    break;
                }
            }
            if (dataGridView2.Rows[transports.Count].Cells[0].Value != null)
                AddTransport();
            FillDataGride();
        }

        private void dataGridView2_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (canDelete)
            {
                try
                {
                    TransportController.Remove(transports[e.RowIndex]);
                    FillDataGride();
                }
                catch (Exception ex)
                {
                    dataGridView2.Rows.Add( 1);
                }
            }
        }
    }        
}


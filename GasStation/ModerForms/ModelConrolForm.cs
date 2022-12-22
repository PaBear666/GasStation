using GasStation.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GasStation
{
    public partial class ModelConrolForm : Form
    {


        DescTopologyClass descTopologyClass;
        int _FuelContarinerLength;
        bool _int = true;
        int ShopsLength;
        DataBaseContext context = new DataBaseContext();
        List<Fuel> fuels;
        Fuel[] FuelForContainer;
        int[] FuelForContainerVolume;
        List<Transport> dbTransports;
        List<Transport> transports = new List<Transport>();
        FileStream fileStream ;
        int[] CashBox;
        int SelectedRadioButton = -1;
        public ModelConrolForm(int fuelContarinerLength,int shopsLength)
        {
            fuels = context.Fuels.ToList();
            dbTransports = context.Transports.ToList();
            InitializeComponent();
            SelectedRadioButton = 0;
            Determ.Checked = true;
            _FuelContarinerLength = fuelContarinerLength;
            ShopsLength = shopsLength;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            for (int i = 0; i < dbTransports.Count; i++)
            {
                listBox2.Items.Add(dbTransports[i].Name);
            }

            FuelForContainer = new Fuel[_FuelContarinerLength];
            FuelForContainerVolume = new int[_FuelContarinerLength];
            readChangesFromFile();
        }

        private void fillDataGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add(_FuelContarinerLength);
            dataGridView2.Rows.Clear();
            dataGridView2.Rows.Add(ShopsLength);
            for (int i =0; i< _FuelContarinerLength;i++)
            {
                dataGridView1.Rows[i].Cells[0].Value= i+1;
                dataGridView1.Rows[i].Cells[1].Value = FuelForContainer[i].Type;
                dataGridView1.Rows[i].Cells[2].Value = FuelForContainerVolume[i];
            }
            for (int i = 0; i < ShopsLength; i++)
            {
                dataGridView2.Rows[i].Cells[0].Value = i + 1;
                dataGridView2.Rows[i].Cells[1].Value = CashBox[i];
            }
        }

        private void Determ_CheckedChanged(object sender, EventArgs e)
        {
            SelectedRadioButton = 0;
            textBox1.Enabled = true;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            comboBox1.Enabled = false;
            writeChagesToFile();
        }

        private void Random_CheckedChanged(object sender, EventArgs e)
        {
            SelectedRadioButton = 1;
            textBox1.Enabled = false;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            comboBox1.Enabled = true;
            comboBox1.SelectedIndex = 0;
            writeChagesToFile();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                if (!listBox1.Items.Contains(listBox2.Items[listBox2.SelectedIndex]))
                {
                    listBox1.Items.Add(listBox2.Items[listBox2.SelectedIndex]);

                    transports.Add(dbTransports[listBox2.SelectedIndex]);
                }
                writeChagesToFile();
            }
        }
         
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                transports.RemoveAt(listBox1.SelectedIndex);
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);

            }
            writeChagesToFile();
        }
        int findFuelID(string s)
        {

            for (int i = 0; i < fuels.Count; i++)
            {
                if (s.Equals(fuels[i].Type))
                    return i;
            }
            return 0;
        }
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 1)
            {
                int r = dataGridView1.CurrentCell.RowIndex;
                if (dataGridView1.Rows[r].Cells[1].Value == null)
                {
                    dataGridView1.Rows[r].Cells[1].Value = fuels[0].Type;
                }
                else
                {
                    string s = dataGridView1.Rows[r].Cells[1].Value.ToString();
                    int i = findFuelID(s);
                    if (e.Button == MouseButtons.Left)
                    {
                       
                            i--;
                            if (i < 0)
                            {
                                dataGridView1.Rows[r].Cells[1].Value = fuels[fuels.Count - 1].Type;
                                FuelForContainer[r] = fuels[fuels.Count - 1];
                            }
                            else
                            {
                                FuelForContainer[r] = fuels[i];
                                dataGridView1.Rows[r].Cells[1].Value = fuels[i].Type;
                            }
                        
                    }
                    if (e.Button == MouseButtons.Right)
                    {
                        
                            i++;
                        if (i >= fuels.Count)
                        {
                            FuelForContainer[r] = fuels[0];
                            dataGridView1.Rows[r].Cells[1].Value = fuels[0].Type;
                        }
                        else
                        {
                            FuelForContainer[r] = fuels[i];
                            dataGridView1.Rows[r].Cells[1].Value = fuels[i].Type;
                        }
                        
                    }
                }
            }
            writeChagesToFile();
        }
        bool containsFuel(Fuel f)
        {
           
            for(int i = 0; i < FuelForContainer.Length; i++)
            {
                if (FuelForContainer[i].Type == f.Type) return true;
            }
            return false;
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
          
            string s = textBox1.Text;
            if (e.KeyChar != 8)
                if (!((char.IsDigit(e.KeyChar))))
                    e.Handled = true;
                else
                    writeChagesToFile();
            else
                writeChagesToFile();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            string s = textBox2.Text;
            if (e.KeyChar != 8)
                if (!((char.IsDigit(e.KeyChar)) || (e.KeyChar == ',' && !s.Contains(",") && s.Length > 0)))
                    e.Handled = true;
                
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            string s = textBox3.Text;
            if (e.KeyChar != 8)
                if (!((char.IsDigit(e.KeyChar)) || (e.KeyChar == ',' && !s.Contains(",") && s.Length > 0)))
                    e.Handled = true;
              
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
           switch(comboBox1.SelectedIndex)
            {
                case 0:
                    {
                        label1.Text = "Математическое ожидание";
                        label2.Text = "Дисперсия";
                        textBox2.Enabled = true;
                        textBox3.Enabled = true;
                        break;
                    }
                case 1:
                    {
                        label1.Text = "λ";
                        label2.Text = "";
                        textBox2.Enabled = true;
                        textBox3.Enabled = false;
                        break;
                    }
                case 2:
                    {
                        label1.Text = "Левая граница";
                        label2.Text = "Правая граница";
                        textBox2.Enabled = true;
                        textBox3.Enabled =  true;
                        break;
                    }
                default:
                    {
                        label1.Text = "";
                        label2.Text = "";
                        textBox2.Enabled = false;
                        textBox3.Enabled = false;
                        break;
                    }

            }
            writeChagesToFile();
        }
       
        void writeChagesToFile()
        {
            if (!_int) { 
            DataGridToArrays();
            descTopologyClass = new DescTopologyClass();
            if (SelectedRadioButton==0)
            {
                descTopologyClass.randomType = DescTopologyClass.RandomType.Fixed;

                if (textBox1.Text == "")
                    descTopologyClass.A = 1;
                else
                    descTopologyClass.A = Double.Parse(textBox1.Text);
            }
                if (SelectedRadioButton == 1)
                {
                    descTopologyClass.randomType = DescTopologyClass.RandomType.Destribution;

                    switch (comboBox1.SelectedIndex)
                    {
                        case 0:
                            {
                                descTopologyClass.destributionType = DescTopologyClass.DestributionType.Normal;

                                if (textBox2.Text == "")
                                    descTopologyClass.A = 1;
                                else
                                    descTopologyClass.A = Double.Parse(textBox2.Text);

                                if (textBox3.Text == "")
                                    descTopologyClass.B = 1;
                                else
                                    descTopologyClass.B = Double.Parse(textBox3.Text);
                                break;
                            }
                        case 1:
                            {
                                descTopologyClass.destributionType = DescTopologyClass.DestributionType.Exp;

                                if (textBox2.Text == "")
                                    descTopologyClass.A = 1;
                                else
                                    descTopologyClass.A = Double.Parse(textBox2.Text);
                                break;
                            }
                        case 2:
                            {
                                descTopologyClass.destributionType = DescTopologyClass.DestributionType.Equels;

                                if (textBox2.Text == "")
                                    descTopologyClass.A = 1;
                                else
                                    descTopologyClass.A = Double.Parse(textBox2.Text);

                                if (textBox3.Text == "")
                                    descTopologyClass.B = 1;
                                else
                                    descTopologyClass.B = Double.Parse(textBox3.Text);
                                break;
                            }
                        default:
                            {

                                break;
                            }
                    }
                }
                
            
            DescTopologyClass.FuelContainer fuel = new DescTopologyClass.FuelContainer();  
            fuel.Fuels = FuelForContainer;
            fuel.Volume = FuelForContainerVolume;
           
            descTopologyClass.fuelContainer = fuel;
            descTopologyClass.Cashbox = CashBox;
            descTopologyClass.Transports = transports.ToArray();

            BinaryFormatter formatter = new BinaryFormatter();
            fileStream = new FileStream("descriptor", FileMode.Create);
            formatter.Serialize(fileStream, descTopologyClass);
            fileStream.Close();
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            if (textBox1.Text != "")
            {
                if (Double.Parse(textBox1.Text) > 60 || Double.Parse(textBox1.Text) < 1)
                {
                    textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length-1,1);
                    textBox1.SelectionStart = textBox1.Text.Length;
                }
            }
            else
                textBox1.Text = "1";
            writeChagesToFile();
        }
        private void readChangesFromFile()
        {
            try
            {
                FileStream fileStream = new FileStream("descriptor", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                descTopologyClass = (DescTopologyClass)formatter.Deserialize(fileStream);
                fileStream.Close();
                dataGridView1.Rows.Clear();
                dataGridView1.Rows.Add(_FuelContarinerLength);
                for(int i =0;i<dataGridView1.Rows.Count;i++)
                {
                    if (i < descTopologyClass.fuelContainer.Fuels.Length)
                    {
                        FuelForContainer[i] = descTopologyClass.fuelContainer.Fuels[i];
                        FuelForContainerVolume[i] = descTopologyClass.fuelContainer.Volume[i];
                    }
                    else
                    {
                        FuelForContainer[i] = fuels[0];
                        FuelForContainerVolume[i] = 400;
                    }
                }
                CashBox = descTopologyClass.Cashbox;
                fillDataGrid();
                transports.AddRange(descTopologyClass.Transports);
                listBox1.Items.Clear();
                for(int i = 0;i<transports.Count;i++)
                    listBox1.Items.Add(transports[i].Name);
                switch(descTopologyClass.randomType)
                {
                    case DescTopologyClass.RandomType.Fixed:
                        {
                            textBox1.Text = descTopologyClass.A.ToString();
                            Determ.Checked = true;
                            break;
                        }
                    case DescTopologyClass.RandomType.Destribution:
                        {
                            Random.Checked = true;
                            switch (descTopologyClass.destributionType)
                            {

                                case DescTopologyClass.DestributionType.Normal:
                                {
                                        
                                    textBox2.Text=descTopologyClass.A.ToString();
                                    textBox3.Text=descTopologyClass.B.ToString();
                                    comboBox1.SelectedIndex = 0;
                                    break;
                                }
                                case DescTopologyClass.DestributionType.Exp:
                                {
                                      
                                        textBox2.Text = descTopologyClass.A.ToString();
                                        comboBox1.SelectedIndex = 1;
                                        break;
                                }
                                case DescTopologyClass.DestributionType.Equels:
                                    {
                                        
                                        textBox2.Text = descTopologyClass.A.ToString();
                                        textBox3.Text = descTopologyClass.B.ToString();
                                        comboBox1.SelectedIndex=2;
                                        break;
                                    }
                            }
                            
                            break;
                        }
                }

            }
            catch (Exception ex)
            {
                for (int i = 0; i < _FuelContarinerLength; i++)
                {
                    FuelForContainer[i] = fuels[0];
                    FuelForContainerVolume[i] = 400;
                }
                CashBox = new int[1];
                CashBox[0] = 1000000;
                fillDataGrid();
                Random.Checked = true;
                Determ.Checked = true;
            }
            _int = false;
            
        }
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            dataGridView1.EditingControl.KeyPress -= Editing1Control_KeyPress;
            dataGridView1.EditingControl.KeyPress += Editing1Control_KeyPress;
        }

        private void Editing1Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && dataGridView1.CurrentCell.ColumnIndex == 2)
            {
                if (!char.IsDigit(e.KeyChar))
                    e.Handled = true;
                else
                {
                    if (dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value != null)
                    {
                        if (int.TryParse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString(), out int v))
                             FuelForContainerVolume[dataGridView1.CurrentCell.RowIndex] = v; 
                        else
                            MessageBox.Show("Значение под номером:" + (dataGridView1.CurrentCell.RowIndex + 1) + "выходит за диапозон");
                    }
                    else
                        FuelForContainerVolume[dataGridView1.CurrentCell.RowIndex] = 0;
                    writeChagesToFile();
                }
                
            }
        }
        private void Editing2Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && dataGridView2.CurrentCell.ColumnIndex == 1)
            {
                if (!char.IsDigit(e.KeyChar))
                    e.Handled = true;
                else
                {
                    if (dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[1].Value != null)
                    {
                        if (int.TryParse(dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[1].Value.ToString(), out int v))
                            CashBox[dataGridView2.CurrentCell.RowIndex] = v;
                        else
                            MessageBox.Show("Значение под номером:" + (dataGridView2.CurrentCell.RowIndex + 1) + "выходит за диапозон");
                    }
                    else
                        CashBox[dataGridView2.CurrentCell.RowIndex] = 0;
                    writeChagesToFile();

                }
            }
          
        }
        private void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            dataGridView2.EditingControl.KeyPress -= Editing2Control_KeyPress;
            dataGridView2.EditingControl.KeyPress += Editing2Control_KeyPress;
        }
        private void DataGridToArrays()
        {
            for(int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (dataGridView2.Rows[i].Cells[1].Value != null)
                {
                    CashBox[i] = int.Parse(dataGridView2.Rows[i].Cells[1].Value.ToString());
                }
                else
                    CashBox[i] = 0;
            }
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[2].Value != null)
                {
                    FuelForContainerVolume[i] = int.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());
                }
                else
                    FuelForContainerVolume[i] = 0;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

            if (textBox2.Text != "")
            {
                if (Double.Parse(textBox2.Text) > 60 || Double.Parse(textBox2.Text) < 1)
                {
                    textBox2.Text = textBox2.Text.Remove(textBox2.Text.Length - 1, 1);
                    textBox2.SelectionStart = textBox2.Text.Length;
                }
            }
            else
                textBox2.Text = "1";
            writeChagesToFile();
            writeChagesToFile();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != 2)
            {
                if (textBox3.Text != "")
                {
                    if (Double.Parse(textBox3.Text) > 60 || Double.Parse(textBox3.Text) < 1d / 3)
                    {
                        textBox3.Text = textBox3.Text.Remove(textBox3.Text.Length - 1, 1);
                        textBox3.SelectionStart = textBox3.Text.Length;
                    }
                }
                else
                    textBox3.Text = "1";
            }
            else
            {
                if (textBox3.Text != "")
                {
                    if (Double.Parse(textBox3.Text) > 60 || Double.Parse(textBox3.Text) < 1)
                    {
                        textBox3.Text = textBox3.Text.Remove(textBox3.Text.Length - 1, 1);
                        textBox3.SelectionStart = textBox3.Text.Length;
                    }
                }
                else
                    textBox3.Text = "1";
            }
            writeChagesToFile();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool flag = true;
            if (SelectedRadioButton == 0)
            {


                if (textBox1.Text == "")
                {
                    MessageBox.Show("Нет значения для детерминорованного закона распределния");
                    flag = false;
                }
                
            }
            if (SelectedRadioButton == 1)
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        {
                            descTopologyClass.destributionType = DescTopologyClass.DestributionType.Normal;

                            if (textBox2.Text == "")
                            {
                                MessageBox.Show("Нет Мат ожидания для нормального закона распределния");
                                flag = false;
                            }

                            if (textBox3.Text == "")
                            {
                                MessageBox.Show("Нет диспресии для нормального закона распределния");
                                flag = false;
                            }
                            break;
                        }
                    case 1:
                        {
                            descTopologyClass.destributionType = DescTopologyClass.DestributionType.Exp;

                            if (textBox2.Text == "")
                            {
                                MessageBox.Show("Нет λ для показательного закона распределния");
                                flag = false;
                            }
                            break;
                        }
                    case 2:
                        {
                            descTopologyClass.destributionType = DescTopologyClass.DestributionType.Equels;

                            if (textBox2.Text == "")
                            {
                                MessageBox.Show("Нет левой границы для равномерного закона распределния");
                                flag = false;
                            }

                            if (textBox3.Text == "")
                            {
                                MessageBox.Show("Нет правой границы для детерминорованного закона распределния");
                                flag = false;
                            }
                            break;
                        }
                    default:
                        {

                            break;
                        }
                        

                }

            }
            if (listBox1.Items.Count <= 0)
            {
                flag = false;
              
                MessageBox.Show("Вы не выбрали модели авто для моделирования");
            }
            if (flag)
            {
                writeChagesToFile();
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}

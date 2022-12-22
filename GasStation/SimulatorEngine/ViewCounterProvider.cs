using GasStation.SimulatorEngine.ApplianceSimulators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GasStation.SimulatorEngine
{
    public class ViewCounterProvider
    {
        public static DataGridView DataGrid { get;set; }
        public static DataGridView DataGridTanker { get; set; }
        public static Label Label { get; set; }
        public static double[] LastCheck { get; set; }
        public static int[] LastFill { get; set; }
        public static void fillDataGridGasStation()
        {
            DataGrid.Rows.Clear();
            DataGrid.Rows.Add(LastCheck.Length);
            for (int i = 0; i < LastCheck.Length; i++)
            {
                DataGrid.Rows[i].Cells[0].Value = i + 1;
                DataGrid.Rows[i].Cells[1].Value = LastFill[i];
                DataGrid.Rows[i].Cells[2].Value = LastCheck[i];
            }   
        }
        public static void fillDataGridTankern()
        {
            DataGridTanker.Rows.Clear();
            DataGridTanker.Rows.Add(TankerConnector.Volume.Length);
            for (int i = 0; i < TankerConnector.Volume.Length; i++)
            {
                DataGridTanker.Rows[i].Cells[0].Value = i + 1;
                if (TankerConnector.Volume[i] < 0) TankerConnector.Volume[i] = 0;
                DataGridTanker.Rows[i].Cells[1].Value = TankerConnector.Volume[i];
                DataGridTanker.Rows[i].Cells[2].Value = TankerConnector.Fuel[i].Type;
                DataGridTanker.Rows[i].Cells[3].Value = TankerConnector.Fuel[i].Cost;
            }
        }
        public static void Fdg()
        {
            Action action = () => fillDataGridGasStation();
            Action action1 = () => fillDataGridTankern();
            Action action2 = () => Label.Text = "Денег в кассе на данный момент:\n" + TankerConnector.CurrentMoney.ToString();
            if (DataGrid.InvokeRequired&& DataGridTanker.InvokeRequired && Label.InvokeRequired)
            {
               
                DataGrid.Invoke(action);
                DataGridTanker.Invoke(action1);
                Label.Invoke(action2);
            }
            else
            {
                action();
                action1();
                action2();
            }
            
        }
    }
}

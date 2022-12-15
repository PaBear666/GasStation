using GasStation.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.SimulatorEngine.ApplianceSimulators
{
    public static class TankerConnector
    {
        public static  int [] Volume { get; set; }
        public static int[] MaxVolume { get; set; }
        public static Fuel [] Fuel { get; set; }
        public static bool[] CanFill { get; set; }
        public static bool[] CanSpawnTankerCar { get; set; }
        public static bool CanSpawnCollectorCar { get; set; }
        public static double CurrentMoney { get; set; }
        public static double MaxMoney { get; set; }
        public static bool MoneyReplacing { get; set; }
        public static double MoneyPrecent
        {
            get { return 0.70d; }
        }
        public static SimulatorSquare DisspawnSquare { get; set; }
        public static int FindFuel(string fuelType)
        {
            for (int i = 0; i < Fuel.Length; i++)
            {
                if (Fuel[i].Type == fuelType)
                {
                    return i;
                }
            }
            return -1;
        }
        

    }
}

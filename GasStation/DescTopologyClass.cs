using GasStation.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation
{
    [Serializable]
    public class DescTopologyClass
    {
        
        
        public  int[] Cashbox { get; set; }
        public RandomType randomType { get; set; }
        public DestributionType destributionType { get; set; }
        public FuelContainer fuelContainer { get; set; }
        public double A { get; set; }
        public double B { get; set; }
        public Transport[] Transports { get; set; }


        [Serializable]
        public enum RandomType
        {
            Fixed,
            Destribution,
            NaN
        }
        [Serializable]
        public enum DestributionType
        {
            Normal,
            Exp,
            Equels,
            NaN
        }
        [Serializable]
        public class FuelContainer
        {
           
            public Fuel[] Fuels { get; set; }
            public int[] Volume { get; set; }
        }




    }
}

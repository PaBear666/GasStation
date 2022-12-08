using GasStation.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
        public static DescTopologyClass GetDesc(string desc)
        {
            try
            {
                FileStream fileStream = new FileStream(desc, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                return (DescTopologyClass)formatter.Deserialize(fileStream);
            }
            catch (Exception ex)
            {
                return null;
            }
        }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.SimulatorEngine.ApplianceSimulators
{
    public class FuelRate
    {
        const int _fuelRate = 10;
        const int _fuelSpeed = 100;
        const double _moneyRate = 1000;
        public static int FuelPower { get { return _fuelRate; } }
        public static int FuelSpeed { get { return _fuelSpeed; } }
        public static double MoneyRate { get { return _moneyRate; } }

    }
}

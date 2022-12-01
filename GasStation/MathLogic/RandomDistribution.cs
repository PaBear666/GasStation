using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;

namespace GasStation.MathLogic
{
    public class RandomDistribution
    {
        public static void Norm()
        {
            double mx = 0;
            double dx = 0;
           Normal normal = new Normal(mx,dx);
        }
    }
}

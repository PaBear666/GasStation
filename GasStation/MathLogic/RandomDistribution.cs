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

        public static double SampleGaussian(Random random, double mean, double stddev)
        {
            // The method requires sampling from a uniform random of (0,1]
            // but Random.NextDouble() returns a sample of [0,1).
            double x1 = 1 - random.NextDouble();
            double x2 = 1 - random.NextDouble();

            double y1 = Math.Sqrt(-2.0 * Math.Log(x1)) * Math.Cos(2.0 * Math.PI * x2);
            return y1 * stddev + mean;
        }
        public static double Uniform(Random random, double a, double b)
        {
            return a + random.NextDouble() * (b - a);

        }
        public double RandExp(Random rnd,double lyambda)
        {
            double xk = rnd.NextDouble();
            return ((-1d / lyambda) * Math.Log(xk));
        }
        public static double ExpDistrib(Random rnd, double lyambda)
        {
            double xk = rnd.NextDouble();
            return ((-1d / lyambda) * Math.Log(xk));
        }

    }
}

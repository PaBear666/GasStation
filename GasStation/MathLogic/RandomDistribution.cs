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
            while (true)
            {
                double x1 = 1 - random.NextDouble();
                double x2 = 1 - random.NextDouble();

                double y1 = Math.Sqrt(-2.0 * Math.Log(x1)) * Math.Cos(2.0 * Math.PI * x2);
             if((y1 * stddev + mean)>0)
                return y1 * stddev + mean;
            }
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
        public static double GetTimeValue(DescTopologyClass desc, Random rnd)
        {
            switch (desc.randomType)
            {
                case DescTopologyClass.RandomType.Fixed:
                {
                        return desc.A;
                  
                }
                case DescTopologyClass.RandomType.Destribution:
                {
                        switch (desc.destributionType)
                        {
                            case DescTopologyClass.DestributionType.Normal:
                                {
                                    return SampleGaussian(rnd, desc.A, desc.B); 
                                }
                            case DescTopologyClass.DestributionType.Exp:
                                {
                                    return ExpDistrib(rnd, desc.A);
                                }
                            case DescTopologyClass.DestributionType.Equels:
                                {
                                    return Uniform(rnd,desc.A,desc.B);
                                }
                                default:
                                {
                                    return desc.A;
                                }
                        }
                }
                    default :
                        return desc.A;
            }

        }

    }
}

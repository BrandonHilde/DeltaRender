using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XDelta
{
    public class DeltaMath
    {
        public static double x = 256;

        public static double Distance(double[] diA, double[] diB) // diA is the first set of dimensions example: (new double[] {x,y,z}, new double[] {x,y,z}) <- for 3D Distance Calculations
        {
            double dist = -1; //use -1 to indicate error

            if (diA.Length != diB.Length) return dist;

            dist = 0;

            double[] df = new double[diA.Length];

            for(int i = 0; i < diA.Length; i++)
            {
                df[i] = diA[i] - diB[i];
                df[i] *= df[i];

                dist += df[i];
            }

            dist = Math.Sqrt(dist);

            return dist;
        }

        public static double Narrow(double x)
        {
            double f = x;

            if (x > 1) f = 1d / f;

            if (f > 1) f = 1;
            if (f < -1) f = -1;

            return f;
        }

        public static double XRandom()
        {
            double a = 2;
            double b = 1;
            double m = 197;

            x = ((a * x) + b) % m;
      
            return x;
        }
    }
}

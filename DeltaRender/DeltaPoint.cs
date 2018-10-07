using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace XDelta
{
    public class DeltaPoint
    {
        public double[] dX { get; set; }

        /// <summary>
        /// Creates a multi-dimensional point.
        /// </summary>
        /// <param name="x">Array of as many dimensional values as you want.</param>
        public DeltaPoint(double[] x)
        {
            dX = x;
        }

        public DeltaPoint(string load)
        {
            string[] vals = load.Split(' ');

            dX = new double[vals.Length];

            for(int i = 0; i < vals.Length; i++)
            {
                dX[i] = Convert.ToDouble(vals[i]);
            }
        }

        /// <summary>
        /// Adds extra dimensions.
        /// </summary>
        /// <param name="ext">Dimension values to add.</param>
        public void Extend(double[] ext)
        {
            double[] final = new double[ext.Length + dX.Length];

            for(int i = 0; i < final.Length; i++)
            {
                if(i < dX.Length)
                {
                    final[i] = dX[i];
                }
                else
                {
                    final[i] = ext[i - dX.Length];
                }
            }

            dX = final;
        }

        public DeltaPoint()
        {
            dX = new double[] {0};
        }

        public void SetRandom(Random r)
        {
            dX = new double[] {r.Next(-100, 100), r.Next(-100, 100), r.Next(-100, 100), r.Next(-100, 100), r.Next(-100, 100) };
        }
    }
}

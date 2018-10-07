using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace XDelta
{
    public class Eye
    {
        public DeltaPoint origin { get; set; }

        private Bitmap btm { get; set; }

        private Graphics graphics { get; set; }

        private DeltaPoint[] dp { get; set; }

        private Model[] WorldObjects { get; set; }

        public Font f = new Font("Arial", 10);

        private double ang = 320.1;

        public Eye()
        {
            origin = new DeltaPoint(new double[] { 0.001, 0.001 });
        }

        /// <summary>
        /// Scales the view perspective.
        /// </summary>
        /// <param name="Scalar"></param>
        public void IncrementViewScalar(double Scalar)
        {
            ang += Scalar;
        }

        /// <summary>
        /// Warps Perspective.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void MovePerspective(double x, double y)
        {
            origin.dX[0] += x;
            origin.dX[1] += y;
        }

        /// <summary>
        /// Resets the perspective.
        /// </summary>
        public void Reset()
        {
            origin = new DeltaPoint(new double[] { 0.001, 0.001 });
            ang = 50;
        }

        /// <summary>
        /// Add models to the renderer.
        /// </summary>
        /// <param name="ModelObjects"></param>
        public void LoadWorldObjects(Model[] ModelObjects)
        {
            WorldObjects = ModelObjects;
        }

        private double[] ConvertDelta(double[] X, double[] A, double Scalar)
        {
            double[] V = new double[A.Length]; /// v = {x,y,0....}

            for (int i = 0; i < V.Length; i++)
            {
                if (i < X.Length)
                {
                    V[i] = X[i];
                }
                else
                {
                    V[i] = 0;
                }
            }

            double sc = DeltaMath.Distance(V, A);
            double comp = ((A[A.Length - 1]) / sc);
            if (comp < 0) comp = -comp;
            sc += comp;

            if (sc != 0)
            {
                sc = (Scalar / sc);
            }
            else
            {
                sc = Scalar / 0.0001;
            }

            double[] cx = new double[X.Length];

            for (int i = 0; i < cx.Length; i++)
            {
                cx[i] = (A[i] * sc);
            }

            if(A.Length > 3) cx = ConvertDelta(origin.dX, cx, sc);

            return cx;
        }

        private DeltaPoint[] ConvertToPoints(DeltaMesh Mesh)
        {
            DeltaPoint[] final = new DeltaPoint[3];

            DeltaPoint[] dp = { Mesh.A, Mesh.B, Mesh.C };

            for (int i = 0; i < dp.Length; i++)
            {
                if (final[i] == null) final[i] = new DeltaPoint();
                final[i].dX = ConvertDelta(origin.dX, dp[i].dX, ang);
            }

            return final;
        }

        
        /// <summary>
        /// Renders information to a bitmap
        /// </summary>
        /// <param name="Screen">Size of the Bitmap</param>
        /// <returns>A rendering of the world objects</returns>
        public Bitmap GiveEyeGraphic(Size Screen)
        {
            // graphics
            if (btm == null)
            {
                btm = new Bitmap(Screen.Width, Screen.Height);
                graphics = Graphics.FromImage(btm);
            }

            graphics.Clear(Color.FromArgb(255, 0, 0, 0));

            graphics.DrawString("X: " 
                + origin.dX[0].ToString("#.##") 
                + " Y: " 
                + origin.dX[1].ToString("#.##") 
                + " Scalar: " 
                + ang.ToString("#.##"), f, Brushes.AliceBlue, 10, 10);

            // screen

            double width = Screen.Width;
            double height = Screen.Height;

            double calc_w = width / 2;
            double calc_h = height / 2;

            //objects

            for (int i = 0; i < WorldObjects.Length; i++)
            {
                 for (int c = 0; c < WorldObjects[i].GetAllMeshes().Length; c++)
                 {
                     dp = ConvertToPoints(WorldObjects[i].GetAllMeshes()[c]);

                     double x1 = (dp[0].dX[0]) + calc_w;
                     double y1 = (dp[0].dX[1]) + calc_h;

                     double x2 = (dp[1].dX[0]) + calc_w;
                     double y2 = (dp[1].dX[1]) + calc_h;

                     double x3 = (dp[2].dX[0]) + calc_w;
                     double y3 = (dp[2].dX[1]) + calc_h;

                     Point[] three = 
                     {
                        new Point((short)x1, (short)y1),
                        new Point((short)x2, (short)y2),
                        new Point((short)x3, (short)y3)
                     };

                     graphics.DrawPolygon(Pens.White, three);
                    //graphics.FillPolygon(Brushes.DarkCyan, three);
                }
            }

            return btm;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XDelta
{
    public class Cube
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public double Width { get; set; }

        public Model CubeModel { get; set; }

        public Cube(double x, double y, double z,int w)
        {
            X = x;
            Y = y;
            Z = z;

            Width = w;

            CubeModel = GenerateModel();
        }

        public Model GenerateModel()
        {
            Random r = new Random();
            Model final;

            DeltaMesh[] pts = new DeltaMesh[12];

            double hW = Width / 2;

            // top plane
            pts[0] = new DeltaMesh
            {
                A = new DeltaPoint(new double[] { X + hW, Y + hW, Z + hW }),
                B = new DeltaPoint(new double[] { X + hW, Y + hW, Z + -hW }),
                C = new DeltaPoint(new double[] { X + -hW, Y + hW, Z + -hW })
            };

            pts[1] = new DeltaMesh
            {
                A = new DeltaPoint(new double[] { X + hW, Y + hW, Z + hW }),
                B = new DeltaPoint(new double[] { X + -hW, Y + hW, Z + hW }),
                C = new DeltaPoint(new double[] { X + -hW, Y + hW, Z + -hW })
            };

            // bottom plane                                               
            pts[2] = new DeltaMesh
            {
                A = new DeltaPoint(new double[] { X + hW, Y + -hW, Z + hW }),
                B = new DeltaPoint(new double[] { X + hW, Y + -hW, Z + -hW }),
                C = new DeltaPoint(new double[] { X + -hW, Y + -hW, Z + -hW })
            };

            pts[3] = new DeltaMesh
            {
                A = new DeltaPoint(new double[] { X + hW, Y + -hW, Z + hW }),
                B = new DeltaPoint(new double[] { X + -hW, Y + -hW, Z + hW }),
                C = new DeltaPoint(new double[] { X + -hW, Y + -hW, Z + -hW })
            };

            // forward plane                                              
            pts[4] = new DeltaMesh //top                                  
            {
                A = new DeltaPoint(new double[] { X + -hW, Y + hW, Z + hW }),
                B = new DeltaPoint(new double[] { X + hW, Y + hW, Z + hW }),
                C = new DeltaPoint(new double[] { X + hW, Y + -hW, Z + hW })
            };

            pts[5] = new DeltaMesh //bot                                  
            {
                A = new DeltaPoint(new double[] { X + -hW, Y + hW, Z + hW }),
                B = new DeltaPoint(new double[] { X + -hW, Y + -hW, Z + hW }),
                C = new DeltaPoint(new double[] { X + hW, Y + -hW, Z + hW })
            };

            // Backward plane                                            
            pts[6] = new DeltaMesh //top                                 
            {
                A = new DeltaPoint(new double[] { X + -hW, Y + hW, Z + -hW }),
                B = new DeltaPoint(new double[] { X + hW, Y + hW, Z + -hW }),
                C = new DeltaPoint(new double[] { X + hW, Y + -hW, Z + -hW })
            };

            pts[7] = new DeltaMesh //bot                                 
            {
                A = new DeltaPoint(new double[] { X + -hW, Y + hW, Z + -hW }),
                B = new DeltaPoint(new double[] { X + -hW, Y + -hW, Z + -hW }),
                C = new DeltaPoint(new double[] { X + hW, Y + -hW, Z + -hW })
            };

            // Right plane                                               
            pts[8] = new DeltaMesh //top                                 
            {
                A = new DeltaPoint(new double[] { X + hW, Y + hW, Z + hW }),
                B = new DeltaPoint(new double[] { X + hW, Y + hW, Z + -hW }),
                C = new DeltaPoint(new double[] { X + hW, Y + -hW, Z + -hW })
            };

            pts[9] = new DeltaMesh //bot                                 
            {
                A = new DeltaPoint(new double[] { X + hW, Y + hW, Z + hW }),
                B = new DeltaPoint(new double[] { X + hW, Y + -hW, Z + hW }),
                C = new DeltaPoint(new double[] { X + hW, Y + -hW, Z + -hW })
            };

            // Left plane                                                
            pts[10] = new DeltaMesh //top                                
            {
                A = new DeltaPoint(new double[] { X + -hW, Y + hW, Z + hW }),
                B = new DeltaPoint(new double[] { X + -hW, Y + hW, Z + -hW }),
                C = new DeltaPoint(new double[] { X + -hW, Y + -hW, Z + -hW })
            };

            pts[11] = new DeltaMesh //bot                                
            {
                A = new DeltaPoint(new double[] { X + -hW, Y + hW, Z + hW }),
                B = new DeltaPoint(new double[] { X + -hW, Y + -hW, Z + hW }),
                C = new DeltaPoint(new double[] { X + -hW, Y + -hW, Z + -hW })
            };

            final = new Model(pts);

            return final;
        }
    }
}

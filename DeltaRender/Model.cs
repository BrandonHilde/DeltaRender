using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XDelta
{
    public class Model
    {
        DeltaMesh[] dm = new DeltaMesh[0];

        public Model()
        {
            dm = new DeltaMesh[1];
        }

        public Model(DeltaMesh[] Mesh)
        {
            dm = Mesh;
        }

        /// <summary>
        /// Loads in a 3D Wavefront Object file (*.obj)
        /// </summary>
        /// <param name="FilePath">File path to the (*.obj) file</param>
        public Model(string FilePath)
        {
            string[] dta = System.IO.File.ReadAllLines(FilePath);

            List<DeltaPoint> verts = new List<DeltaPoint>();
            List<DeltaMesh> meshes = new List<DeltaMesh>();

            for (int i = 0; i < dta.Length; i++)
            {
                if (dta[i].Substring(0, 2) == "v ")
                {
                    verts.Add(new DeltaPoint(dta[i].Substring(2)));
                }

                if (dta[i].Substring(0, 2) == "f ")
                {
                    int[] cords = importFace(dta[i].Substring(2));

                    meshes.Add(new DeltaMesh
                    {
                        A = verts[cords[0] - 1],
                        B = verts[cords[1] - 1],
                        C = verts[cords[2] - 1]
                    });
                }
            }

            dm = meshes.ToArray();
        }

        private int[] importFace(string face)
        {
            int[] i = { 0, 0, 0 };

            string[] spl = face.Split(' ');

            char c = '/';

            for (int x = 0; x < 3; x++)
            {
                i[x] = Convert.ToInt16(spl[x].Split(c)[0]);
            }


            return i;
        }

        /// <summary>
        /// Adds model coordinates as extra dimensional points.
        /// </summary>
        /// <param name="extModel">The model to add to the current model.</param>
        public void Extend(Model extModel)
        {
            DeltaMesh[] extMesh = extModel.CopyMeshes();
            DeltaMesh[] nDM = this.CopyMeshes();

            for (int i = 0; i < nDM.Length; i++)
            {
                if (i < extMesh.Length)
                {
                    nDM[i].A.Extend(extMesh[i].A.dX);
                    nDM[i].B.Extend(extMesh[i].B.dX);
                    nDM[i].C.Extend(extMesh[i].C.dX);
                }
            }

            dm = nDM;
        }

        /// <summary>
        /// Adds coordinates as extra dimensional points.
        /// </summary>
        /// <param name="extD">The coordinates to add to the current model.</param>
        public void Extend(double[] extD)
        {
            for (int i = 0; i < dm.Length; i++)
            {
                dm[i].A.Extend(extD);
                dm[i].B.Extend(extD);
                dm[i].C.Extend(extD);
            }
        }

        public void SetRandom(int n)
        {
            Random r = new Random();
            dm = new DeltaMesh[n];

            for (int i = 0; i < dm.Length; i++)
            {
                dm[i] = new DeltaMesh();
                dm[i].SetRandom(r);
            }
        }

        public DeltaMesh[] GetAllMeshes()
        {
            return dm;
        }

        public DeltaMesh[] CopyMeshes()
        {
            DeltaMesh[] meshes = new DeltaMesh[dm.Length];

            for (int i = 0; i < dm.Length; i++)
            {
                meshes[i] = dm[i].Copy();
            }

            return meshes;
        }
    }

    public class DeltaMesh
    {
        public DeltaPoint A { get; set; }
        public DeltaPoint B { get; set; }
        public DeltaPoint C { get; set; }

        public void SetRandom(Random r)
        {
            A = new DeltaPoint(); A.SetRandom(r);
            B = new DeltaPoint(); B.SetRandom(r);
            C = new DeltaPoint(); C.SetRandom(r);
        }

        public DeltaMesh Copy()
        {
            return new DeltaMesh
            {
                A = A.Copy(),
                B = B.Copy(),
                C = C.Copy()
            };
        }
    }
}
